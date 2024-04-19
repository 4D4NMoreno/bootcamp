using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BootcampContext _context;

    public TransactionRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<bool> MakeTransfer(TransferRequest transferRequest)
    {
        var originAccount = await _context.Accounts.Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .Where(a => a.Id == transferRequest.OriginAccountId)
            .FirstOrDefaultAsync();
        if (originAccount == null || originAccount.Status == AccountStatus.Inactive)
        {
            throw new BusinessLogicException($"The account with id: {transferRequest.OriginAccountId} does not exist or is inactive.");
        }
        if (originAccount.Customer == null)
        {
            throw new BusinessLogicException($"The account with id: {transferRequest.OriginAccountId} does not have a customer assigned.");
        }
        if (originAccount.Balance < transferRequest.Amount)
        {
            throw new BusinessLogicException("Insufficient balance.");
        }

        Account destinationAccount;
        if (originAccount.Customer?.Bank?.Name == transferRequest.DestinationBank)
        {
            destinationAccount = await _context.Accounts
                .Where(a => a.Id == transferRequest.DestinationAccountId &&
                            a.Customer.Bank.Name == transferRequest.DestinationBank &&
                            a.CurrencyId == transferRequest.CurrencyId)
                .FirstOrDefaultAsync();
        }
        else
        {
            destinationAccount = await _context.Accounts
                .Include(a => a.Customer.Bank)
                .Where(a => a.Customer.Bank.Name == transferRequest.DestinationBank &&
                            a.Number == transferRequest.DestinationAccountNumber &&
                            a.Customer.DocumentNumber == transferRequest.DestinationDocumentNumber &&
                            a.CurrencyId == transferRequest.CurrencyId)
                .FirstOrDefaultAsync();
        }

        if (destinationAccount == null || destinationAccount.Status == AccountStatus.Inactive)
        {
            throw new BusinessLogicException($"The destination account does not exist or is inactive.");
        }

        if (originAccount.Type != destinationAccount.Type ||
            originAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            throw new BusinessLogicException("Not applicable.");
        }

        var currency = await _context.Currencies.FindAsync(destinationAccount.CurrencyId);

        var transfer = transferRequest.Adapt<Transaction>();

        originAccount.Balance -= transferRequest.Amount;
        destinationAccount.Balance += transferRequest.Amount;

        var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(transfer.TransactionDateTime, TimeZoneInfo.Local);

        var originMovement = new Movement
        {
            AccountId = transferRequest.OriginAccountId,
            Destination = transferRequest.DestinationAccountNumber,
            Amount = -transferRequest.Amount,
            TransferredDateTime = localDateTime,
            TransferStatus = TransferStatus.Done
        };

        var destinationMovement = new Movement
        {
            AccountId = destinationAccount.Id,
            Amount = transferRequest.Amount,
            TransferredDateTime = localDateTime,
            TransferStatus = TransferStatus.Done
        };

        _context.Transactions.Add(transfer);
        _context.Movements.AddRange(originMovement, destinationMovement);
        await _context.SaveChangesAsync();

        return true;
    }

}

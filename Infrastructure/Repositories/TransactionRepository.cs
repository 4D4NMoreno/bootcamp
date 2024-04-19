using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Request;
using Infrastructure.Contexts;
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
        var originAccount = await _context.Accounts.FindAsync(transferRequest.OriginAccountId);
        if (originAccount == null || originAccount.Status == AccountStatus.Inactive)
        {
            return false;
        }

        if (originAccount.Balance < transferRequest.Amount)
        {
            return false;
        }
        var destinationAccount = await _context.Accounts
    .Include(a => a.Customer.Bank)
    .Where(a => a.Id == transferRequest.DestinationAccountId &&
                a.Customer.Bank.Name == transferRequest.DestinationBank &&
                a.Number == transferRequest.DestinationAccountNumber &&
                a.Customer.DocumentNumber == transferRequest.DestinationDocumentNumber &&
                a.Currency.Id == transferRequest.CurrencyId)
    .FirstOrDefaultAsync();

        if (destinationAccount == null)
        {
            throw new Exception("No se encontró la cuenta de destino.");
        }

        if (originAccount.Type != destinationAccount.Type ||
            originAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            
            return false;
        }
        var transfer = new Transaction
        {
            OriginAccountId = transferRequest.OriginAccountId,
            DestinationAccountId = destinationAccount.Id,
            Amount = transferRequest.Amount,
            TransactionDateTime = DateTime.Now
        };

        originAccount.Balance -= transferRequest.Amount;
        destinationAccount.Balance += transferRequest.Amount;

        var originMovement = new Movement
        {
            AccountId = transferRequest.OriginAccountId,
            Destination = transferRequest.DestinationAccountNumber,
            Amount = -transferRequest.Amount,
            TransferredDateTime = DateTime.Now,
            TransferStatus = TransferStatus.Done
        };

        var destinationMovement = new Movement
        {
            AccountId = destinationAccount.Id,
            Amount = transferRequest.Amount,
            TransferredDateTime = DateTime.Now,
            TransferStatus = TransferStatus.Done
        };

        _context.Transactions.Add(transfer);
        _context.Movements.AddRange(originMovement, destinationMovement);

        await _context.SaveChangesAsync();

        return true;
    }
}

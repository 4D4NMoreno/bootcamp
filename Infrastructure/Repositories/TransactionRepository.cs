﻿using Core.Constants;
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
    public async Task<bool> MakeTransfer(int originAccountId, TransferRequest transferRequest)
    {
        var originAccount = await _context.Accounts.FindAsync(originAccountId);
        if (originAccount == null || originAccount.Status == AccountStatus.Inactive)
        {
            return false;
        }

        if (originAccount.Balance < transferRequest.Amount)
        {
            return false;
        }

        var destinationAccount = await _context.Accounts
            .Where(a => a.Customer.Bank.Name == transferRequest.DestinationBank &&
                        a.Number == transferRequest.DestinationAccountNumber &&
                        a.Customer.DocumentNumber == transferRequest.DestinationDocumentNumber &&
                        a.CurrencyId == transferRequest.CurrencyId)
            .FirstOrDefaultAsync();

        if (destinationAccount == null)
        {
            return false;
        }

        if (originAccount.Type != destinationAccount.Type ||
            originAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            
            return false;
        }
        var transfer = new Transaction
        {
            OriginAccountId = originAccountId,
            DestinationAccountId = destinationAccount.Id,
            Amount = transferRequest.Amount,
            TransactionDateTime = DateTime.Now
        };

        originAccount.Balance -= transferRequest.Amount;
        destinationAccount.Balance += transferRequest.Amount;

        var originMovement = new Movement
        {
            AccountId = originAccountId,
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

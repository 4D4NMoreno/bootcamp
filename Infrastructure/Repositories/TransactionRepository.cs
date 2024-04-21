using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BootcampContext _context;

    public TransactionRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<TransferDTO> MakeTransfer(TransferRequest transferRequest)
    {

        var originAccount = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == transferRequest.OriginAccountId);


        var destinationAccount = _context.Accounts
            .FirstOrDefault(a => a.Id == transferRequest.DestinationAccountId);

        var transfer = transferRequest.Adapt<Transaction>();

        originAccount.Balance -= transferRequest.Amount;
        destinationAccount.Balance += transferRequest.Amount;


        var localDateTime = TimeZoneInfo.ConvertTimeFromUtc
            (transfer.TransactionDateTime, TimeZoneInfo.Local);


        var destination = await _context.Accounts
            .Where(a => a.Holder == destinationAccount.Holder)
            .FirstOrDefaultAsync();

        var destinationName = destination != null ? destination.Holder : null;

        var originMovement = new Movement
        {
            
            AccountId = transferRequest.OriginAccountId,
            Destination = $"Account: {destinationAccount.Holder} Number:" +
            $" ({destinationAccount.Number})",
            Amount = transferRequest.Amount,
            TransferredDateTime = localDateTime,
            TransferStatus = TransferStatus.Done,
            MovementType = MovementType.Transfer
        };

        var destinationMovement = new Movement
        {
            AccountId = destinationAccount.Id,
            Amount = transferRequest.Amount,
            TransferredDateTime = localDateTime,
            TransferStatus = TransferStatus.Done,
            MovementType = MovementType.Transfer
        };
        _context.Transactions.Add(transfer);
        _context.Movements.AddRange(originMovement, destinationMovement);
        await _context.SaveChangesAsync();

        var TransferDTO = new TransferDTO
        {
            Id = originMovement.Id,
            AccountId = transferRequest.OriginAccountId,
            Destination = originMovement.Destination,
            Amount = originMovement.Amount,
            TransferredDateTime = localDateTime,
            TransferStatus = originMovement.TransferStatus.ToString(),
            MovementType = originMovement.MovementType.ToString()
        };
        
       
        return TransferDTO;
    }

    public async Task<(bool isValid, string message)> ValidateTransferRequest(TransferRequest transferRequest)
    {
        var originAccount = _context.Accounts
          .Include(a => a.Customer)
          .ThenInclude(c => c.Bank)
          .FirstOrDefault(a => a.Id == transferRequest.OriginAccountId);

        var destinationAccount = _context.Accounts
           .Include(a => a.Customer)
           .ThenInclude(c => c.Bank)
           .FirstOrDefault(a => a.Id == transferRequest.DestinationAccountId);

        if (originAccount == null)
        {

            return (false, $"The origin account with id: {transferRequest.OriginAccountId} does not exist");
        }

        if (originAccount.Balance < transferRequest.Amount)
        {
            return (false, "Insufficient balance in the origin account.");
        }

        if (originAccount.Status == AccountStatus.Inactive)
        {
            return (false, $"The origin account with id: {transferRequest.OriginAccountId} is inactive.");
        }

        if (destinationAccount == null || destinationAccount.Status == AccountStatus.Inactive)
        {
            return (false, "The destination account does not exist or is inactive.");
        }

        if (originAccount.Type != destinationAccount.Type ||
            originAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            return (false, "The origin and destination accounts are not applicable for the transfer.");
        }

        if (originAccount.Customer?.BankId == transferRequest.DestinationBank)
        {
            destinationAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == transferRequest.DestinationAccountId &&
                                          a.Customer.BankId == transferRequest.DestinationBank)
            ?? throw new BusinessLogicException
                  ("No se encontró la cuenta de destino.");
        }
        else
        {
            destinationAccount = await _context.Accounts
                .FirstOrDefaultAsync
                (a => a.Customer.BankId == transferRequest.DestinationBank &&
                 a.Number == transferRequest.DestinationAccountNumber &&
                 a.Customer.DocumentNumber == transferRequest.DestinationDocumentNumber)
            ?? throw new BusinessLogicException
                  ("No se encontró la cuenta de destino.");
        }
        return (true, "all validations are correct");
    }
    public async Task<bool> MakePayment(PaymentRequest paymentRequest)
    {
  
        {
            var validationResult = await ValidatePaymentRequest(paymentRequest);

            if (validationResult.isValid is false)
            {
                return false;
            }

            var debitedAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == paymentRequest.DebitedAccountId);

            if (debitedAccount == null || debitedAccount.Balance < paymentRequest.Amount)
            {
                return false;
            }

            debitedAccount.Balance -= paymentRequest.Amount;

            var payment = new Transaction
            {
                OriginAccountId = paymentRequest.DebitedAccountId,
                Amount = paymentRequest.Amount,
                TransactionDateTime = paymentRequest.TransactionDateTime,
                //DocumentNumber = paymentRequest.DocumentNumber,
                //Description = paymentRequest.Description
            };

            var movement = new Movement
            {
                AccountId = paymentRequest.DebitedAccountId,
                Amount = paymentRequest.Amount,
                TransferredDateTime = paymentRequest.TransactionDateTime,
                TransferStatus = TransferStatus.Done,
                MovementType = MovementType.PaymentsForServices
            };

            _context.Transactions.Add(payment);
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();

            return true;
        }
  
    }
    

    private async Task<(bool isValid, string message)> ValidatePaymentRequest(PaymentRequest paymentRequest)
    {
        if (string.IsNullOrEmpty(paymentRequest.DocumentNumber))
        {
            return (false, "Document number is required.");
        }

        if (paymentRequest.Amount <= 0)
        {
            return (false, "Invalid amount.");
        }

        if (string.IsNullOrEmpty(paymentRequest.Description))
        {
            return (false, "Description is required.");
        }

        var debitedAccount = await _context.Accounts.FirstOrDefaultAsync
            (a => a.Id == paymentRequest.DebitedAccountId);

        if (debitedAccount == null)
        {
            return (false, "Invalid debited account.");
        }


        return (true, "All validations are correct.");
    }
}




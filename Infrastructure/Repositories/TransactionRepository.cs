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
           .Include(a => a.CurrentAccount)
           .FirstOrDefaultAsync(a => a.Id == transferRequest.OriginAccountId);

        var destinationAccount = await _context.Accounts
            .Include(a => a.CurrentAccount)
            .FirstOrDefaultAsync
            (a => a.Id == transferRequest.DestinationAccountId);

        var transfer = transferRequest.Adapt<Transaction>();

        originAccount.Balance -= transferRequest.Amount;
        destinationAccount.Balance += transferRequest.Amount;

        if (originAccount.CurrentAccount != null)
        {
            originAccount.CurrentAccount.OperationalLimit -= transferRequest.Amount;
            destinationAccount.CurrentAccount.OperationalLimit -= transferRequest.Amount;
        }

        var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(transfer.TransactionDateTime, TimeZoneInfo.Local);

        var originMovement = new Movement
        {
            AccountId = transferRequest.OriginAccountId,
            Destination = $"Holder: {destinationAccount.Holder}, Number: ({destinationAccount.Number})",
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

        var transferDTO = new TransferDTO
        {
            Id = originMovement.Id,
            AccountId = transferRequest.OriginAccountId,
            Destination = originMovement.Destination,
            Amount = originMovement.Amount,
            TransferredDateTime = localDateTime,
            TransferStatus = originMovement.TransferStatus.ToString(),
            MovementType = originMovement.MovementType.ToString()
        };

        return transferDTO;
    }

    public async Task<(bool isValid, string message)> ValidateTransferRequest(TransferRequest transferRequest)
    {
        var originAccount = await _context.Accounts
            .Include(a => a.CurrentAccount)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(a => a.Id == transferRequest.OriginAccountId);

        var destinationAccount = await _context.Accounts
            .Include(a => a.CurrentAccount)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(a => a.Id == transferRequest.DestinationAccountId);
       
        if (originAccount == null || destinationAccount == null )
        {
            return (false, $"The source or destination account does not exist");
        }

        if (originAccount.Status == AccountStatus.Inactive)
        {
            return (false, $"The vaccount with id: {transferRequest.OriginAccountId} is inactive.");
        }

        if (destinationAccount.Status == AccountStatus.Inactive)
        {
            return (false, $"The account with id: {transferRequest.DestinationAccountId} is inactive.");
        }

        if (originAccount.Balance < transferRequest.Amount)
        {
            return (false, "Insufficient balance in the origin account.");
        }

        

        if (originAccount.Type != destinationAccount.Type
           || originAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            return (false, "The origin and destination accounts are not applicable for the transfer.");
        }

        if (originAccount.CurrentAccount != null)
        {
            if (transferRequest.Amount > originAccount.CurrentAccount.OperationalLimit
                || transferRequest.Amount > destinationAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "operational limit reached");
            }
        }

        if (originAccount.Customer?.BankId == transferRequest.DestinationBank)
        {
            destinationAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == transferRequest.DestinationAccountId &&
                a.Customer.BankId == transferRequest.DestinationBank)
                ?? throw new BusinessLogicException("Destination account not found.");
        }
        else
        {
            destinationAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Customer.BankId == transferRequest.DestinationBank &&
                                          a.Number == transferRequest.DestinationAccountNumber &&
                                          a.Customer.DocumentNumber == transferRequest.DestinationDocumentNumber)
                ?? throw new BusinessLogicException("Destination account not found.");
        }

        return (true, "All validations are correct.");
    }

    public async Task<PaymentDTO> MakePayment(PaymentRequest paymentRequest)
    {
  
            var originAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == paymentRequest.OriginAccountId);

            if (originAccount.CurrentAccount != null)
            {
            originAccount.CurrentAccount.OperationalLimit -= paymentRequest.Amount;
            }

            originAccount.Balance -= paymentRequest.Amount;

            var payment = paymentRequest.Adapt<Transaction>();

            payment.TransactionType = TransactionType.PaymentsForServices;

            var movement = new Movement
            {
                AccountId = paymentRequest.OriginAccountId,
                Destination = paymentRequest.Description,
                Amount = paymentRequest.Amount,
                TransferredDateTime = paymentRequest.TransactionDateTime,
                TransferStatus = TransferStatus.Done,
                MovementType = MovementType.PaymentsForServices
            };

            _context.Transactions.Add(payment);
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();

            var paymentDTO = new PaymentDTO
            {
                MovementType = movement.MovementType.ToString(),
                OriginAccount = $"Holder: {originAccount.Holder}, Number: ({originAccount.Number})",
                Amount = paymentRequest.Amount,
                DocumentNumber = paymentRequest.DocumentNumber,
                Description = paymentRequest.Description,
                TransactionDateTime = paymentRequest.TransactionDateTime,
            };

            return paymentDTO;
  
    }
    
    public async Task<(bool isValid, string message)> ValidatePaymentRequest(PaymentRequest paymentRequest)
    {
        var originAccount = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == paymentRequest.OriginAccountId);

        if (originAccount == null)
        {
            return (false, "Origin Account is required.");
        }

        if (paymentRequest.DocumentNumber == null)
        {
            return (false, "Document number is required.");
        }

        if (paymentRequest.Description == null)
        {
            return (false, "Description is required.");
        }

        if (originAccount.Customer.DocumentNumber != paymentRequest.DocumentNumber)
        {
            return (false, "Invalid Document Number.");
        }

        if (originAccount.Balance < paymentRequest.Amount)
        {
            return (false, "Insufficient balance in the origin account.");
        }
        if (originAccount.CurrentAccount != null)
        {
            if (paymentRequest.Amount > originAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "operational limit reached");
            }
        }

        return (true, "All validations are correct.");
    }

    public async Task<DepositDTO> MakeDeposit(DepositRequest depositRequest)
    {
        var destinationAccount = await _context.Accounts
                .Include(a => a.Customer)
                .ThenInclude(c => c.Bank)
                .FirstOrDefaultAsync(a => a.Id == depositRequest.DestinationAccountId);

        if (destinationAccount.CurrentAccount != null)
        {
            destinationAccount.CurrentAccount.OperationalLimit -= depositRequest.Amount;
        }

        destinationAccount.Balance += depositRequest.Amount;

        var deposit = depositRequest.Adapt<Transaction>();

        deposit.TransactionType = TransactionType.Deposit;

        var movement = new Movement
        {
            AccountId = depositRequest.DestinationAccountId,
            Destination = $"Holder: {destinationAccount.Holder}, Number: ({destinationAccount.Number})",
            Amount = depositRequest.Amount,
            TransferredDateTime = depositRequest.TransactionDateTime,
            TransferStatus = TransferStatus.Done,
            MovementType = MovementType.Deposit
        };

        _context.Transactions.Add(deposit);
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        var depositDTO = new DepositDTO
        {
            MovementType = movement.MovementType.ToString(),
            DestinationAccount = $"Holder: {destinationAccount.Holder}, Number: ({destinationAccount.Number})",
            Amount = depositRequest.Amount,
            Bank = destinationAccount.Customer.Bank.Name.ToString(),
            TransactionDateTime = depositRequest.TransactionDateTime,
        };

        return depositDTO;
    }

    public async Task<(bool isValid, string message)> ValidateDepositRequest(DepositRequest depositRequest)
    {
        var destinationAccount = await _context.Accounts
                .Include(a => a.Customer)
                .ThenInclude(c => c.Bank)
                .FirstOrDefaultAsync(a => a.Id == depositRequest.DestinationAccountId);

        if (destinationAccount == null)
        {
            return (false, "Origin Account is required.");
        }

        if (destinationAccount.Customer.BankId != depositRequest.BankId)
        {
            return (false, "The destination bank does not match that of the destination account");
        }

        if (destinationAccount.CurrentAccount != null)
        {
            if (depositRequest.Amount > destinationAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "operational limit reached");
            }
        }

        return (true, "All validations are correct.");
    }

    public async Task<WithdrawalDTO> MakeWithdrawal(WithdrawalRequest withdrawalRequest)
    {
        var originAccount = await _context.Accounts
               .Include(a => a.Customer)
               .ThenInclude(c => c.Bank)
               .FirstOrDefaultAsync(a => a.Id == withdrawalRequest.OriginAccountId);

        if (originAccount.CurrentAccount != null)
        {
            originAccount.CurrentAccount.OperationalLimit -= withdrawalRequest.Amount;
        }

        originAccount.Balance -= withdrawalRequest.Amount;

        var deposit = withdrawalRequest.Adapt<Transaction>();

        deposit.TransactionType = TransactionType.Withdrawal;

        var movement = new Movement
        {
            AccountId = withdrawalRequest.OriginAccountId,
            Destination = $"Holder: {originAccount.Holder}, Number: ({originAccount.Number})",
            Amount = withdrawalRequest.Amount,
            TransferredDateTime = withdrawalRequest.TransactionDateTime,
            TransferStatus = TransferStatus.Done,
            MovementType = MovementType.Withdrawal
        };

        _context.Transactions.Add(deposit);
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        var withdrawalDTO = new WithdrawalDTO
        {
            MovementType = movement.MovementType.ToString(),
            OriginAccount = $"Holder: {originAccount.Holder}, Number: ({originAccount.Number})",
            Amount = withdrawalRequest.Amount,
            Bank = originAccount.Customer.Bank.Name.ToString(),
            TransactionDateTime = withdrawalRequest.TransactionDateTime,
        };

        return withdrawalDTO;
    }

    public async Task<(bool isValid, string message)> ValidateWithdrawalRequest(WithdrawalRequest withdrawalRequest)
    {
        var originAccount = await _context.Accounts
                .Include(a => a.Customer)
                .ThenInclude(c => c.Bank)
                .FirstOrDefaultAsync(a => a.Id == withdrawalRequest.OriginAccountId);

        if (originAccount == null)
        {
            return (false, "Origin Account is required.");
        }

        if (originAccount.Customer.BankId != withdrawalRequest.BankId)
        {
            return (false, "The destination bank does not match that of the destination account");
        }

        if (originAccount.CurrentAccount != null)
        {
            if (withdrawalRequest.Amount > originAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "operational limit reached");
            }
        }

        return (true, "All validations are correct.");
    }
} 





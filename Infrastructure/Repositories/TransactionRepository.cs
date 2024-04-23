using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

        var originAccount = await GetAccountByIdAsync(transferRequest.OriginAccountId);

        var destinationAccount = await GetAccountByIdAsync(transferRequest.DestinationAccountId);

        var transfer = transferRequest.Adapt<Transaction>();

        transfer.Bank = destinationAccount.Customer.Bank.Name;

        if (originAccount.Customer.Bank == destinationAccount.Customer.Bank)
        {
            transfer.DestinationAccountNumber = string.Empty;
            transfer.DestinationDocumentNumber = string.Empty;
        }

        originAccount.Balance -= transferRequest.Amount;

        destinationAccount.Balance += transferRequest.Amount;

        var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(transfer.TransactionDateTime, TimeZoneInfo.Local);

        var originMovement = new Movement
        {
            AccountId = transferRequest.OriginAccountId,
            Destination = $"Holder: {destinationAccount.Holder}," +
                          $" Number: ({destinationAccount.Number})",
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
            Id = transfer.Id,
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
        var originAccount = await GetAccountByIdAsync(transferRequest.OriginAccountId);

        var destinationAccount = await GetAccountByIdAsync(transferRequest.DestinationAccountId);

        if (originAccount.Status == AccountStatus.Inactive || destinationAccount.Status == AccountStatus.Inactive)
        {
            return (false, "Inactive account");
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
                                          a.Customer.DocumentNumber ==
                                          transferRequest.DestinationDocumentNumber)
                ?? throw new BusinessLogicException("Destination account not found.");
        }

        if (originAccount.CurrentAccount != null)
        {
            var transactionSums = GetTransactionSumsForTransfer(transferRequest);

            var originTransactionsSum = transactionSums.originSum;
            var destinationTransactionsSum = transactionSums.destinationSum;

            if (originTransactionsSum + transferRequest.Amount >
                originAccount.CurrentAccount.OperationalLimit ||
                destinationTransactionsSum + transferRequest.Amount >
                destinationAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "Operational limit reached");
            }
        }

        return (true, "All validations are correct.");
    }

    public async Task<PaymentDTO> MakePayment(PaymentRequest paymentRequest)
    {

        var originAccount = await GetAccountByIdAsync(paymentRequest.OriginAccountId);


        originAccount.Balance -= paymentRequest.Amount;

            var payment = paymentRequest.Adapt<Transaction>();

            payment.Bank = originAccount.Customer.Bank.Name;

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
                OriginAccount = $"Holder: {originAccount.Holder}," +
                                $" Number: ({originAccount.Number})",
                Amount = paymentRequest.Amount,
                DocumentNumber = paymentRequest.DocumentNumber,
                Description = paymentRequest.Description,
                TransactionDateTime = paymentRequest.TransactionDateTime,
            };

            return paymentDTO;
  
    }
    
    public async Task<(bool isValid, string message)> ValidatePaymentRequest(PaymentRequest paymentRequest)
    {
        var originAccount = await GetAccountByIdAsync(paymentRequest.OriginAccountId);

        if (paymentRequest.DocumentNumber != originAccount.Customer.DocumentNumber)
        {
            return (false, "Invalid Document Number.");
        }

        if (paymentRequest.Description == null)
        {
            return (false, "Description is required.");
        }

        if (originAccount.Balance < paymentRequest.Amount)
        {
            return (false, "Insufficient balance in the origin account.");
        }

        if (originAccount.CurrentAccount != null)
        {

            var today = paymentRequest.TransactionDateTime;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var originTransactionsSum = _context.Movements
                .Where(t => t.AccountId == paymentRequest.OriginAccountId &&
                       t.TransferredDateTime >= firstDayOfMonth &&
                       t.TransferredDateTime < lastDayOfMonth)
                .Sum(t => t.Amount);

            if (originTransactionsSum + paymentRequest.Amount >
                originAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "Operational limit reached");
            }

        }

        return (true, "All validations are correct.");
    }

    public async Task<DepositDTO> MakeDeposit(DepositRequest depositRequest)
    {
        var destinationAccount = await GetAccountByIdAsync(depositRequest.DestinationAccountId);

        destinationAccount.Balance += depositRequest.Amount;

        var deposit = depositRequest.Adapt<Transaction>();

        deposit.Bank = destinationAccount.Customer.Bank.Name;

        deposit.TransactionType = TransactionType.Deposit;

        var movement = new Movement
        {
            AccountId = depositRequest.DestinationAccountId,
            Destination = $"Holder: {destinationAccount.Holder}," +
                          $" Number: ({destinationAccount.Number})",
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
            DestinationAccount = movement.Destination,
            Amount = depositRequest.Amount,
            Bank = destinationAccount.Customer.Bank.Name.ToString(),
            TransactionDateTime = depositRequest.TransactionDateTime,
        };

        return depositDTO;
    }

    public async Task<(bool isValid, string message)> ValidateDepositRequest(DepositRequest depositRequest)
    {

        var destinationAccount = await GetAccountByIdAsync(depositRequest.DestinationAccountId);

        if (destinationAccount == null)
        {
            return (false, "Destination account not found.");
        }

        if (destinationAccount.Customer.BankId != depositRequest.BankId)
        {
            return (false, "The destination bank does not match that of the destination account");
        }

        if (destinationAccount.CurrentAccount != null)
        {

            var today = depositRequest.TransactionDateTime;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var originTransactionsSum = _context.Movements
                .Where(t => t.AccountId == depositRequest.DestinationAccountId &&
                       t.TransferredDateTime >= firstDayOfMonth &&
                       t.TransferredDateTime < lastDayOfMonth)
                .Sum(t => t.Amount);

            if (originTransactionsSum + depositRequest.Amount >
                destinationAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "Operational limit reached");
            }
        }

        return (true, "All validations are correct.");
    }

    public async Task<WithdrawalDTO> MakeWithdrawal(WithdrawalRequest withdrawalRequest)
    {
        var originAccount = await GetAccountByIdAsync(withdrawalRequest.OriginAccountId);

        originAccount.Balance -= withdrawalRequest.Amount;

        var withdrawal = withdrawalRequest.Adapt<Transaction>();

        withdrawal.Bank = originAccount.Customer.Bank.Name;

        withdrawal.TransactionType = TransactionType.Withdrawal;

        var movement = new Movement
        {
            AccountId = withdrawalRequest.OriginAccountId,
            Destination = $"Holder: {originAccount.Holder}," +
                          $" Number: ({originAccount.Number})",
            Amount = withdrawalRequest.Amount,
            TransferredDateTime = withdrawalRequest.TransactionDateTime,
            TransferStatus = TransferStatus.Done,
            MovementType = MovementType.Withdrawal
        };

        _context.Transactions.Add(withdrawal);
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        var withdrawalDTO = new WithdrawalDTO
        {
            MovementType = movement.MovementType.ToString(),
            OriginAccount = $"Holder: {originAccount.Holder}," +
                            $" Number: ({originAccount.Number})",
            Amount = withdrawalRequest.Amount,
            Bank = originAccount.Customer.Bank.Name.ToString(),
            TransactionDateTime = withdrawalRequest.TransactionDateTime,
        };

        return withdrawalDTO;
    }

    public async Task<(bool isValid, string message)> ValidateWithdrawalRequest(WithdrawalRequest withdrawalRequest)
    {

        var originAccount = await GetAccountByIdAsync(withdrawalRequest.OriginAccountId);

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
            var today = withdrawalRequest.TransactionDateTime;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var originTransactionsSum = _context.Movements
                .Where(t => t.AccountId == withdrawalRequest.OriginAccountId &&
                       t.TransferredDateTime >= firstDayOfMonth &&
                       t.TransferredDateTime < lastDayOfMonth)
                .Sum(t => t.Amount);

            if (originTransactionsSum + withdrawalRequest.Amount >
                originAccount.CurrentAccount.OperationalLimit)
            {
                return (false, "Operational limit reached");
            }
        }

        return (true, "All validations are correct.");
    }

    public async Task<List<TransactionDTO>> GetFilteredTransactions(FilterTransactionModel filter)
    {

        var transactionsQuery = _context.Transactions
            .Where(t => t.OriginAccountId == filter.AccountId || 
                   t.DestinationAccountId == filter.AccountId)
            .AsQueryable();

        if (filter.AccountId == 0)
        {
            throw new ArgumentException("AccountId is required");
        }

        if (filter.Month.HasValue && filter.Year.HasValue)
        {
            var startDate = new DateTime(filter.Year.Value, filter.Month.Value, 1, 0, 0, 0, DateTimeKind.Utc);

            var endDate = startDate.AddMonths(1).AddDays(-1);

            transactionsQuery = transactionsQuery
                                    .Where(t => t.TransactionDateTime >= startDate &&
                                           t.TransactionDateTime <= endDate);
        }
        else if (filter.Month.HasValue || filter.Year.HasValue)
        {
            throw new ArgumentException("Both month and year should be specified if one is provided.");
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            string filterDescriptionLower = filter.Description.ToLower();

            transactionsQuery = transactionsQuery
                                    .Where(x => x.Description.ToLower() ==
                                                filterDescriptionLower);
        }

            var transactions = await transactionsQuery.ToListAsync();

        var transactionDTOs = transactions.Select(t =>
        {
            var transactionDTO = t.Adapt<TransactionDTO>();
            transactionDTO.TransactionType = t.TransactionType.ToString();
            transactionDTO.DestinationBank = t.Bank ??= string.Empty;
            transactionDTO.DestinationAccountNumber ??= string.Empty;
            transactionDTO.DestinationDocumentNumber ??= string.Empty;
            transactionDTO.Description ??= string.Empty;
            
            return transactionDTO;
        }).ToList();

        return transactionDTOs;
    }

    public (decimal originSum, decimal destinationSum) GetTransactionSumsForTransfer(TransferRequest transferRequest)
    {
        var today = transferRequest.TransactionDateTime;
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        var originTransactionsSum = _context.Movements
            .Where(t => t.AccountId == transferRequest.OriginAccountId &&
                   t.TransferredDateTime >= firstDayOfMonth &&
                   t.TransferredDateTime < lastDayOfMonth)
            .Sum(t => t.Amount);

        var destinationTransactionsSum = _context.Movements
            .Where(t => t.AccountId == transferRequest.DestinationAccountId &&
                   t.TransferredDateTime >= firstDayOfMonth &&
                   t.TransferredDateTime < lastDayOfMonth)
            .Sum(t => t.Amount);

        return (originTransactionsSum, destinationTransactionsSum);
    }
    private async Task<Account> GetAccountByIdAsync(int accountId)
    {
        return await _context.Accounts
             .Include(a => a.CurrentAccount)
             .Include(a => a.Customer)
             .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(a => a.Id == accountId) ??
            throw new BusinessLogicException("account not found.");
    }

}


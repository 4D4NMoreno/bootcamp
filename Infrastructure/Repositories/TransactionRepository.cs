using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
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

        var originMovement = transferRequest.Adapt<Movement>();
        originMovement.AccountId = transferRequest.OriginAccountId;

        originMovement.Destination = Destination(originAccount);

        var destinationMovement = transferRequest.Adapt<Movement>();

        destinationMovement.AccountId = transferRequest.DestinationAccountId;

        _context.Transactions.Add(transfer);

        _context.Movements.AddRange(originMovement, destinationMovement);

        await _context.SaveChangesAsync();

        var transferDTO = transfer.Adapt<TransferDTO>();

        transferDTO.Destination = originMovement.Destination;

        transferDTO.TransferStatus = originMovement.TransferStatus.ToString();

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

        var service = await _context.Services
                     .FirstOrDefaultAsync(a => a.Id == paymentRequest.ServiceId);

        var movement = paymentRequest.Adapt<Movement>();

        movement.Destination = service.ServiceName;
        //movement.Destination = paymentRequest.ServiceId.ToString();

        var paymentDTO = payment.Adapt<PaymentDTO>();

        paymentDTO.OriginAccount = Destination(originAccount);

        paymentDTO.Description = $"Pago de Servicio = {movement.Destination}";

        payment.Description = paymentDTO.Description;

        _context.Transactions.Add(payment);
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        return paymentDTO;

    }

    public async Task<(bool isValid, string message)> ValidatePaymentRequest(PaymentRequest paymentRequest)
    {
        var originAccount = await GetAccountByIdAsync(paymentRequest.OriginAccountId);

        if (paymentRequest.DocumentNumber != originAccount.Customer.DocumentNumber)
        {
            return (false, "Invalid Document Number.");
        }

        var service = await _context.Services
                     .FirstOrDefaultAsync(a => a.Id == paymentRequest.ServiceId);

        if (service == null)
        {
            return (false, "Service not found");
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

        var movement = depositRequest.Adapt<Movement>();

        movement.Destination = Destination(destinationAccount);

        _context.Transactions.Add(deposit);
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        var depositDTO = deposit.Adapt<DepositDTO>();

        depositDTO.DestinationAccount = movement.Destination;
        depositDTO.Bank = destinationAccount.Customer.Bank.Name;
        depositDTO.MovementType = movement.MovementType.ToString();

        return depositDTO;
    }

    public async Task<(bool isValid, string message)> ValidateDepositRequest(DepositRequest depositRequest)
    {

        var destinationAccount = await GetAccountByIdAsync(depositRequest.DestinationAccountId);

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

        var movement = withdrawalRequest.Adapt<Movement>();

        _context.Transactions.Add(withdrawal);
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        var withdrawalDTO = withdrawal.Adapt<WithdrawalDTO>();

        withdrawalDTO.OriginAccount = Destination(originAccount);
        withdrawalDTO.Bank = originAccount.Customer.Bank.Name;
        withdrawalDTO.MovementType = movement.MovementType.ToString();

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
        if (originAccount.Balance < withdrawalRequest.Amount)
        {
            return (false, "Insufficient balance in the origin account.");
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

    //public async Task<List<TransactionDTO>> GetFilteredTransactions(FilterTransactionModel filter)
    //{

    //    var transactionsQuery = _context.Transactions
    //        .Where(t => t.OriginAccountId == filter.AccountId || 
    //               t.DestinationAccountId == filter.AccountId)
    //        .AsQueryable();

    //    if (filter.AccountId == 0)
    //    {
    //        throw new ArgumentException("AccountId is required");
    //    }

    //    if (filter.Month.HasValue && filter.Year.HasValue)
    //    {
    //        var startDate = new DateTime(filter.Year.Value, filter.Month.Value, 1, 0, 0, 0, DateTimeKind.Utc);

    //        var endDate = startDate.AddMonths(1).AddDays(-1);

    //        transactionsQuery = transactionsQuery
    //                                .Where(t => t.TransactionDateTime >= startDate &&
    //                                       t.TransactionDateTime <= endDate);
    //    }
    //    else if (filter.Month.HasValue || filter.Year.HasValue)
    //    {
    //        throw new ArgumentException("Both month and year should be specified if one is provided.");
    //    }

    //    if (!string.IsNullOrEmpty(filter.Description))
    //    {
    //        string filterDescriptionLower = filter.Description.ToLower();

    //        transactionsQuery = transactionsQuery
    //                                .Where(x => x.Description.ToLower() ==
    //                                            filterDescriptionLower);
    //    }

    //        var transactions = await transactionsQuery.ToListAsync();

    //    var transactionDTOs = transactions.Select(t =>
    //    {
    //        var transactionDTO = t.Adapt<TransactionDTO>();
    //        transactionDTO.TransactionType = t.TransactionType.ToString();
    //        transactionDTO.DestinationBank = t.Bank ??= string.Empty;
    //        transactionDTO.DestinationAccountNumber ??= string.Empty;
    //        transactionDTO.DestinationDocumentNumber ??= string.Empty;
    //        transactionDTO.Description ??= string.Empty;

    //        return transactionDTO;
    //    }).ToList();

    //    return transactionDTOs;
    //}

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
    private string Destination(Account account)
    {
        var destination = $"Id: {account.Id}, " +
                          $"Holder: {account.Holder}, " +
                          $"Number: ({account.Number})";

        return destination;
    }

    public async Task<List<MovementDTO>> GetFilteredMovements(FilterTransactionModel filter)
    {
        var movementsQuery = _context.Movements
            .Where(m => m.AccountId == filter.AccountId)
            .AsQueryable();

        if (filter.AccountId == 0)
        {
            throw new BusinessLogicException("AccountId is required");
        }

        if (filter.Month.HasValue && filter.Year.HasValue)
        {
            var startDate = new DateTime(filter.Year.Value, filter.Month.Value, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            movementsQuery = movementsQuery
                .Where(m => m.TransferredDateTime >= startDate && m.TransferredDateTime <= endDate);
        }
        else if (filter.Month.HasValue || filter.Year.HasValue)
        {
            throw new BusinessLogicException("Both month and year should be specified if one is provided.");
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            string filterDescriptionLower = filter.Description.ToLower();
            string filterDescription = filterDescriptionLower
                   .First().ToString().ToUpper() + filterDescriptionLower.Substring(1);

            var movements = await movementsQuery.ToListAsync();

            movements = movements
                .Where(m => Enum.GetName(typeof(MovementType), m.MovementType)
                .Equals(filterDescription, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var movementDTOs = movements.Select(m =>
            {
                var movementDTO = new MovementDTO
                {
                    AccountId = m.AccountId,
                    Description = Enum.GetName(typeof(MovementType), m.MovementType),
                    Amount = m.Amount,
                    TransferredDateTime = m.TransferredDateTime,
                    TransferStatus = m.TransferStatus.ToString()
                };

                return movementDTO;
            }).ToList();

            return movementDTOs;
        }

        var allMovements = await movementsQuery.ToListAsync();

        var allMovementDTOs = allMovements.Select(m =>
        {
            var movementDTO = new MovementDTO
            {
                AccountId = m.AccountId,
                Description = m.MovementType.ToString(),
                Amount = m.Amount,
                TransferredDateTime = m.TransferredDateTime,
                TransferStatus = m.TransferStatus.ToString()
            };

            return movementDTO;
        }).ToList();

        return allMovementDTOs;
    }
}


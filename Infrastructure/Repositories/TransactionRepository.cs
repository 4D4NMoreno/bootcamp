using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
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
    public async Task<TransferDTO> MakeTransfer(TransferRequest transferRequest)
    {

        var originAccount = await GetAccountByIdAsync(transferRequest.OriginAccountId);

        var destinationAccount = await GetAccountByIdAsync(transferRequest.DestinationAccountId);

        originAccount.Balance -= transferRequest.Amount;

        destinationAccount.Balance += transferRequest.Amount;

        var originMovement = transferRequest.Adapt<Movement>();

        originMovement.AccountId = transferRequest.OriginAccountId;

        originMovement.Destination = Destination(destinationAccount);

        var destinationMovement = transferRequest.Adapt<Movement>();

        destinationMovement.AccountId = transferRequest.DestinationAccountId;

        var transfer = transferRequest.Adapt<Transaction>();

        transfer.Bank = destinationAccount.Customer.Bank.Name;

        transfer.Description = originMovement.MovementType.ToString();

        if (originAccount.Customer.Bank == destinationAccount.Customer.Bank)
        {
            transfer.DestinationAccountNumber = string.Empty;
            transfer.DestinationDocumentNumber = string.Empty;
        }

        _context.Transactions.Add(transfer);

        _context.Movements.AddRange(originMovement, destinationMovement);

        await _context.SaveChangesAsync();

        var transferDTO = transfer.Adapt<TransferDTO>();

        transferDTO.DestinationAccount = originMovement.Destination;

        transferDTO.TransferStatus = originMovement.TransferStatus.ToString();

        return transferDTO;
    }

    public async Task<(bool isValid, string message)> ValidateTransfer(TransferRequest transferRequest)
    {
        var originAccount = await GetAccountByIdAsync(transferRequest.OriginAccountId);

        var destinationAccount = await GetAccountByIdAsync(transferRequest.DestinationAccountId);

        if(originAccount == destinationAccount)
        {
            return (false, "Same account");
        }

        if (originAccount.Status == AccountStatus.Inactive ||
            destinationAccount.Status == AccountStatus.Inactive)
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
                .FirstOrDefaultAsync(a => a.Id == transferRequest.DestinationAccountId)
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

            var today = transferRequest.TransactionDateTime;

            var originTransactionsSum = CalculateTransactionSumForMonth
                                        (today, transferRequest.OriginAccountId);

            var destinationTransactionsSum = CalculateTransactionSumForMonth
                                             (today, transferRequest.DestinationAccountId);

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

        var service = await _context.Services
                     .FirstOrDefaultAsync(a => a.Id == paymentRequest.ServiceId);

        var movement = paymentRequest.Adapt<Movement>();

        movement.Destination = service.ServiceName;

        var payment = paymentRequest.Adapt<Transaction>();

        payment.Bank = originAccount.Customer.Bank.Name;

        payment.Description = movement.MovementType.ToString();

        var paymentDTO = payment.Adapt<PaymentDTO>();

        paymentDTO.OriginAccount = Destination(originAccount);

        paymentDTO.Description = $"Pago de Servicio = {movement.Destination}";

        payment.Description = paymentDTO.Description;

        _context.Transactions.Add(payment);

        _context.Movements.Add(movement);

        await _context.SaveChangesAsync();

        return paymentDTO;

    }

    public async Task<(bool isValid, string message)> ValidatePayment(PaymentRequest paymentRequest)
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

        return (true, "All validations are correct.");
    }

    public async Task<DepositDTO> MakeDeposit(DepositRequest depositRequest)
    {
        var destinationAccount = await GetAccountByIdAsync(depositRequest.DestinationAccountId);

        destinationAccount.Balance += depositRequest.Amount;

        var movement = depositRequest.Adapt<Movement>();

        movement.Destination = Destination(destinationAccount);

        var deposit = depositRequest.Adapt<Transaction>();

        deposit.Bank = destinationAccount.Customer.Bank.Name;

        deposit.Description = movement.MovementType.ToString();

        _context.Transactions.Add(deposit);

        _context.Movements.Add(movement);

        await _context.SaveChangesAsync();

        var depositDTO = deposit.Adapt<DepositDTO>();

        depositDTO.DestinationAccount = movement.Destination;

        depositDTO.Bank = destinationAccount.Customer.Bank.Name;

        depositDTO.MovementType = movement.MovementType.ToString();

        return depositDTO;
    }

    public async Task<(bool isValid, string message)> ValidateDeposit(DepositRequest depositRequest)
    {

        var destinationAccount = await GetAccountByIdAsync(depositRequest.DestinationAccountId);

        if (destinationAccount.Customer.BankId != depositRequest.BankId)
        {
            return (false, "The destination bank does not match that of the destination account");
        }

        if (destinationAccount.CurrentAccount != null)
        {
            var today = depositRequest.TransactionDateTime;

            var originTransactionsSum = CalculateTransactionSumForMonth
                                        (today, depositRequest.DestinationAccountId);

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

        var movement = withdrawalRequest.Adapt<Movement>();

        var withdrawal = withdrawalRequest.Adapt<Transaction>();

        withdrawal.Bank = originAccount.Customer.Bank.Name;

        withdrawal.Description = movement.MovementType.ToString();

        _context.Transactions.Add(withdrawal);

        _context.Movements.Add(movement);

        await _context.SaveChangesAsync();

        var withdrawalDTO = withdrawal.Adapt<WithdrawalDTO>();

        withdrawalDTO.OriginAccount = Destination(originAccount);

        withdrawalDTO.Bank = originAccount.Customer.Bank.Name;

        withdrawalDTO.MovementType = movement.MovementType.ToString();

        return withdrawalDTO;
    }

    public async Task<(bool isValid, string message)> ValidateWithdrawal(WithdrawalRequest withdrawalRequest)
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

            var originTransactionsSum = CalculateTransactionSumForMonth
                                        (today, withdrawalRequest.OriginAccountId);

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
            if (filter.Year < 1 || filter.Month < 1 || filter.Month > 12)
            {
                throw new ArgumentException("Invalid year or month.");
            }

            var startDate = new DateTime(filter.Year.Value,
                                         filter.Month.Value, 1, 0, 0, 0, DateTimeKind.Utc);

            var endDate = startDate.AddMonths(1).AddDays(-1);

            transactionsQuery = transactionsQuery
                                    .Where(t => t.TransactionDateTime >= startDate &&
                                           t.TransactionDateTime <= endDate);
        }
        else if (filter.Month.HasValue || filter.Year.HasValue)
        {
            throw new ArgumentException("Both month and year should be specified if one is provided.");
        }

        if (filter.StartDate.HasValue)
        {
            var startDate = filter.StartDate.Value.ToUniversalTime().Date;

            transactionsQuery = transactionsQuery
                .Where(t => t.TransactionDateTime >= startDate); 
        }
        if (filter.EndDate.HasValue )
        {
            var endDate = filter.EndDate.Value.ToUniversalTime().Date;

            transactionsQuery =  transactionsQuery
                .Where (t => t.TransactionDateTime <= endDate);
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
                transactionDTO.Bank = t.Bank ??= string.Empty;
                transactionDTO.DestinationAccountNumber ??= string.Empty;
                transactionDTO.DestinationDocumentNumber ??= string.Empty;

            return transactionDTO;
        }).ToList();

        return transactionDTOs;
    }

    public decimal CalculateTransactionSumForMonth(DateTime transactionDate, int accountId)
    {
        var firstDayOfMonth = new DateTime(transactionDate.Year, transactionDate.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        lastDayOfMonth = lastDayOfMonth.AddDays(1).Date;

        decimal transactionSum = _context.Movements
        .Where(t => t.AccountId == accountId &&
                    t.TransferredDateTime >= firstDayOfMonth &&
                    t.TransferredDateTime < lastDayOfMonth &&
                    t.MovementType != MovementType.PaymentsForServices)
               .Sum(t => t.Amount);

        return transactionSum;
    }

    private async Task<Account> GetAccountByIdAsync(int accountId)
    {
        var account = await _context.Accounts
        .Include(a => a.CurrentAccount)
        .Include(a => a.Customer)
        .ThenInclude(c => c.Bank)
        .FirstOrDefaultAsync(a => a.Id == accountId);

        if (account is null)
        {
            throw new BusinessLogicException("Account not found.");
        }

        if (account.IsDeleted)
        {
            throw new BusinessLogicException($"The account with ID : {accountId} is deleted.");
        }

        return account;
    }

    private string Destination(Account account)
    {
        var destination = $"Holder: {account.Holder}, " +
                          $"Number: ({account.Number})";

        return destination;
    }
    

    //public async Task<List<MovementDTO>> GetFilteredMovements(FilterTransactionModel filter)
    //{
    //    var movementsQuery = _context.Movements
    //        .Where(m => m.AccountId == filter.AccountId)
    //        .AsQueryable();

    //    if (filter.AccountId == 0)
    //    {
    //        throw new BusinessLogicException("AccountId is required");
    //    }

    //    if (filter.Month.HasValue && filter.Year.HasValue)
    //    {
    //        var startDate = new DateTime(filter.Year.Value, filter.Month.Value, 1, 0, 0, 0, DateTimeKind.Utc);
    //        var endDate = startDate.AddMonths(1).AddDays(-1);

    //        movementsQuery = movementsQuery
    //            .Where(m => m.TransferredDateTime >= startDate && m.TransferredDateTime <= endDate);
    //    }
    //    else if (filter.Month.HasValue || filter.Year.HasValue)
    //    {
    //        throw new BusinessLogicException("Both month and year should be specified if one is provided.");
    //    }

    //    if (!string.IsNullOrEmpty(filter.Description))
    //    {
    //        string filterDescriptionLower = filter.Description.ToLower();
    //        string filterDescription = filterDescriptionLower
    //               .First().ToString().ToUpper() + filterDescriptionLower.Substring(1);

    //        var movements = await movementsQuery.ToListAsync();

    //        movements = movements
    //            .Where(m => Enum.GetName(typeof(MovementType), m.MovementType)
    //            .Equals(filterDescription, StringComparison.OrdinalIgnoreCase))
    //            .ToList();

    //        var movementDTOs = movements.Select(m =>
    //        {
    //            var movementDTO = new MovementDTO
    //            {
    //                AccountId = m.AccountId,
    //                Description = Enum.GetName(typeof(MovementType), m.MovementType),
    //                Amount = m.Amount,
    //                TransferredDateTime = m.TransferredDateTime,
    //                TransferStatus = m.TransferStatus.ToString()
    //            };

    //            return movementDTO;
    //        }).ToList();

    //        return movementDTOs;
    //    }

    //    var allMovements = await movementsQuery.ToListAsync();

    //    var allMovementDTOs = allMovements.Select(m =>
    //    {
    //        var movementDTO = new MovementDTO
    //        {
    //            AccountId = m.AccountId,
    //            Description = m.MovementType.ToString(),
    //            Amount = m.Amount,
    //            TransferredDateTime = m.TransferredDateTime,
    //            TransferStatus = m.TransferStatus.ToString()
    //        };

    //        return movementDTO;
    //    }).ToList();

    //    return allMovementDTOs;
    //}
}


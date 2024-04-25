
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<TransferDTO> MakeTransfer(TransferRequest transferRequest);

    Task<(bool isValid, string message)> ValidateTransfer(TransferRequest transferRequest);

    Task<PaymentDTO> MakePayment(PaymentRequest paymentRequest);

    Task<(bool isValid, string message)> ValidatePayment(PaymentRequest paymentRequest);

    Task<DepositDTO> MakeDeposit(DepositRequest depositRequest);

    Task<(bool isValid, string message)> ValidateDeposit(DepositRequest depositRequest);

    Task<WithdrawalDTO> MakeWithdrawal(WithdrawalRequest withdrawalRequest);

    Task<(bool isValid, string message)> ValidateWithdrawal(WithdrawalRequest withdrawalRequest);

    Task<List<TransactionDTO>> GetFilteredTransactions(FilterTransactionModel filter);

    //Task<List<MovementDTO>> GetFilteredMovements(FilterTransactionModel filter);

}

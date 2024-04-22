
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<TransferDTO> MakeTransfer(TransferRequest transferRequest);

    Task<(bool isValid, string message)> ValidateTransferRequest(TransferRequest transferRequest);

    Task<PaymentDTO> MakePayment(PaymentRequest paymentRequest);

    Task<(bool isValid, string message)> ValidatePaymentRequest(PaymentRequest paymentRequest);

    Task<DepositDTO> MakeDeposit(DepositRequest depositRequest);

    Task<(bool isValid, string message)> ValidateDepositRequest(DepositRequest depositRequest);

    Task<WithdrawalDTO> MakeWithdrawal(WithdrawalRequest withdrawalRequest);

    Task<(bool isValid, string message)> ValidateWithdrawalRequest(WithdrawalRequest withdrawalRequest);
}

using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface ITransactionService
{
    Task<TransferDTO> MakeTransfer(TransferRequest transferRequest);
    Task<PaymentDTO> MakePayment(PaymentRequest paymentRequest);
    Task<DepositDTO> MakeDeposit(DepositRequest DepositRequest);
    Task<WithdrawalDTO> MakeWithdrawal(WithdrawalRequest withdrawalRequest);
    Task<List<MovementDTO>> GetFilteredMovements(FilterTransactionModel filter);
}


using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<TransferDTO> MakeTransfer(TransferRequest transferRequest);

    Task<(bool isValid, string message)> ValidateTransferRequest(TransferRequest transferRequest);

    Task<bool> MakePayment(PaymentRequest paymentRequest);
}

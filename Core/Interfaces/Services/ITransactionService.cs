using Core.Request;

namespace Core.Interfaces.Services;

public interface ITransactionService
{
    Task<bool> MakeTransfer(int originAccountId, TransferRequest transferRequest);
}

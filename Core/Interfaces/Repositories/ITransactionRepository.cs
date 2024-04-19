using Core.Request;

namespace Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<bool> MakeTransfer(TransferRequest transferRequest);
}

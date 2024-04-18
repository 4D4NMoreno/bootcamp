using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Request;

namespace Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }
    public Task<bool> MakeTransfer(int originAccountId, TransferRequest transferRequest)
    {
        throw new NotImplementedException();
    }
}

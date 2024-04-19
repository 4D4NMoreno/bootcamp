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
    public async Task<bool> MakeTransfer(TransferRequest transferRequest)
    {
        return await _repository.MakeTransfer(transferRequest);
    }
}

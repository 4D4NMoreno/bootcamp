using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface ITransactionService
{
    Task<TransferDTO> MakeTransfer(TransferRequest transferRequest);

    
}

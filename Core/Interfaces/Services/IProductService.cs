using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IProductService
{
    Task<ProductRequestDTO> Add(BankProductRequest request);
}

using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IProductService
{
    Task<ProductDTO> Add(BankProductRequest request);
}

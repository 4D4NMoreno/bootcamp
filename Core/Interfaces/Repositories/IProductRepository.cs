using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IProductRepository
{
    Task<ProductDTO> Add(BankProductRequest request);
}

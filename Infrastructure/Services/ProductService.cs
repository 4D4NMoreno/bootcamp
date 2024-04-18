using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<ProductRequestDTO> Add(BankProductRequest request)
    {

        return await _repository.Add(request);
    }
}

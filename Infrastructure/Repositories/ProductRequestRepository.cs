using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRequestRepository : IProductRepository
{
    private readonly BootcampContext _context;

    public ProductRequestRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<ProductRequestDTO> Add(CreateProductRequest request)
    {

        if (!Enum.IsDefined(typeof(ProductName), request.ProductName))
        {
            throw new BusinessLogicException("ProductName not found");
        }

        var customer = await _context.Customers.FindAsync(request.CustomerId);

        if (customer == null)
        {
            throw new BusinessLogicException("Customer not found");
        }

        var currency = await _context.Currencies.FindAsync(request.CurrencyId);


        if (currency == null)
        {
            throw new BusinessLogicException("Currency not found");
        }

        var product = request.Adapt<ProductRequest>();

        _context.ProductRequests.Add(product);

        await _context.SaveChangesAsync();

        var productDTO = request.Adapt<ProductRequestDTO>();

        return productDTO;
    }
}

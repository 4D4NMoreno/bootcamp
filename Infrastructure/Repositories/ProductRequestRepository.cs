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
        var product = await _context.Products.FindAsync(request.ProductId);

        if (request.ProductId != product.Id)
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

        var productRequest = request.Adapt<ProductRequest>();

        _context.ProductRequests.Add(productRequest);

        await _context.SaveChangesAsync();

        //var createdProduct = productRequest.Adapt<ProductRequestDTO>();

        var productDTO = productRequest.Adapt<ProductRequestDTO>();

        return productDTO;
    }
}

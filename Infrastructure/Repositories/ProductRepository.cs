using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly BootcampContext _context;

    public ProductRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<ProductRequestDTO> Add(BankProductRequest request)
    {
        
        var product = request.Adapt<ProductRequest>();

        _context.ProductRequests.Add(product);

        await _context.SaveChangesAsync();

        var createdProduct = await _context.ProductRequests
        .Include(pr => pr.Currency) 
        .Include(pr => pr.Customer) 
            .ThenInclude(c => c.Bank) 
        .FirstOrDefaultAsync(pr => pr.Id == product.Id);


        var productDTO = createdProduct.Adapt<ProductRequestDTO>();

        return productDTO;
    }
}

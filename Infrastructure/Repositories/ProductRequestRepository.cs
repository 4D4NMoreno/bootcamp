using Core.Entities;
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
        
        var product = request.Adapt<ProductRequest>();

        _context.ProductRequests.Add(product);

        await _context.SaveChangesAsync();

        var createdProduct = await _context.ProductRequests
            .Include(pr => pr.Currency)
            .Include(pr => pr.Customer)
        .FirstOrDefaultAsync(pr => pr.Id == product.Id);


        var productDTO = createdProduct.Adapt<ProductRequestDTO>();

        return productDTO;
    }
}

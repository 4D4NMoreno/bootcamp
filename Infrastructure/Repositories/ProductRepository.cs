using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly BootcampContext _context;

    public ProductRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<ProductDTO> Add(BankProductRequest request)
    {
        var product = request.Adapt<Product>();

        if (product.ProductType == ProductType.Credit)
        {
            product.CreditProduct = request.CreateCreditProduct.Adapt<CreditProduct>();
        }
        else if (product.ProductType == ProductType.CreditCard)
        {
            product.CreditCardProduct = request.CreateCreditCardProduct.Adapt<CreditCardProduct>();
        }
        else if (product.ProductType == ProductType.CurrentAccount)
        {
            product.CurrentAccountProduct = request.CreateCurrentAccountProduct.Adapt<CurrentAccountProduct>();
        }

        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        var createdProduct = await _context.Products
            .Include(p => p.CreditProduct)
            .Include(p => p.CreditCardProduct)
            .Include(p => p.CurrentAccountProduct)
            .FirstOrDefaultAsync();

        return createdProduct.Adapt<ProductDTO>();
    }
}

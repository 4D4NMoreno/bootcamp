using Core.Entities;
using Core.Models;
using Core.Request;
using Core.Requests;
using Mapster;

namespace Infrastructure.Mappings;

public class ProductMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BankProductRequest, Product>()
            
            .Map(dest => dest.ProductType, src => src.ProductType);

        config.NewConfig<CreateCreditProduct, CreditProduct>()
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.RequestDate, src => src.RequestDate)
            .Map(dest => dest.ApprovalDate, src => src.ApprovalDate)
            .Map(dest => dest.Term, src => src.Term)
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.CurrencyId, src => src.CurrencyId)
            .Map(dest => dest.BankId, src => src.BankId);

        config.NewConfig<Product, ProductDTO>()
            .Map(dest => dest.ProductType, src => src.ProductType)
            .Map(dest => dest.CreditProduct, src =>
                src.CreditProduct != null
                ? src.CreditProduct
                : null);



        config.NewConfig<CreditProduct, CreditProductDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Term, src => src.Term)
            .Map(dest => dest.RequestDate, src => src.RequestDate)
            .Map(dest => dest.ApprovalDate, src => src.ApprovalDate)
            .Map(dest => dest.Bank, src => src.Bank)
            .Map(dest => dest.Currency, src => src.Currency)
            .Map(dest => dest.Customer, src => src.Customers)
            .Map(dest => dest.Bank, src => src.Bank);

    }
}

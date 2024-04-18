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
        config.NewConfig<BankProductRequest, ProductRequest>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.CurrencyId, src => src.CurrencyId)
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ApprovalDate, src => src.ApprovalDate)
            .Map(dest => dest.ApplicationDate, src => src.ApplicationDate);


        config.NewConfig<ProductRequest, ProductRequestDTO>()
            .Map(dest => dest.ProductName, src => src.ProductName.ToString())
            .Map(dest => dest.Currency, src => src.Currency)
            .Map(dest => dest.Customer, src => src.Customer)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ApprovalDate, src => src.ApprovalDate)
            .Map(dest => dest.ApplicationDate, src => src.ApplicationDate);


    }
}

using Core.Entities;
using Core.Models;
using Core.Request;
using Core.Requests;
using Mapster;
using System.Xml.Linq;

namespace Infrastructure.Mappings;

public class ProductMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductRequest, ProductRequest>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.CurrencyId, src => src.CurrencyId)
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.Description, src => src.Description)
            //.Map(dest => dest.ApprovalDate, src => src.ApprovalDate)
            .Map(dest => dest.ApplicationDate, src => src.ApplicationDate);


        config.NewConfig<Core.Entities.ProductRequest, ProductRequestDTO>()
            .Map(dest => dest.ProductName, src => src.ProductName.ToString())
            .Map(dest => dest.Currency, src => src.Currency.Name)
            .Map(dest => dest.Customer, src => new
            {
                Id = src.Customer.Id,
                Name = src.Customer.Name,
                Lastname = src.Customer.Lastname,
                DocumentNumber = src.Customer.DocumentNumber
            })
            .Map<string, string>(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ApprovalDate, src => src.ApprovalDate)
            .Map(dest => dest.ApplicationDate, src => src.ApplicationDate);


    }
}

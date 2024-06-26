﻿using Core.Entities;
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
        config.NewConfig<CreateProductRequest, ProductRequest>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.CurrencyId, src => src.CurrencyId)
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ApplicationDate, src => src.ApplicationDate);


        config.NewConfig<ProductRequest, ProductRequestDTO>()
            .Map(dest => dest.Currency, src => src.Currency.Name)
            .Map(dest => dest.ProductName, src => src.Product.ProductName)
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

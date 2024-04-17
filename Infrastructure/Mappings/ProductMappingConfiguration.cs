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
            .Map(dest => dest.Term, src => src.Term);

        config.NewConfig<Product, ProductDTO>()
            .Map(dest => dest.ProductType, src => src.ProductType)
            .Map(dest => dest.CreditProduct, src =>
                src.CreditProduct != null
                ? src.CreditProduct
                : null);



        config.NewConfig<SavingAccount, SavingAccountDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.SavingType, src => src.SavingType.ToString())
            .Map(dest => dest.HolderName, src => src.HolderName);

        config.NewConfig<CurrentAccount, CurrentAccountDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.OperationalLimit, src => src.OperationalLimit)
            .Map(dest => dest.MonthAverage, src => src.MonthAverage)
            .Map(dest => dest.Interest, src => src.Interest);
    }
}

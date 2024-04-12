using Core.Constants;
using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class AccountMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<CreateAccountModel, Account>()
                .Map(dest => dest.Holder, src => src.Holder)
                .Map(dest => dest.Number, src => src.Number)
                .Map(dest => dest.Type, src => Enum.Parse<AccountType>(src.AccountType))
                .Map(dest => dest.CurrencyId, src => src.CurrencyId)

                .Map(dest => dest.CustomerId, src => src.CustomerId);

        //.Map(dest => dest.IsDeleted, src => src.IsDeleted)
        //.Map(dest => dest.SavingAccount, src => src.Adapt<SavingAccountDTO>())
        //.Map(dest => dest.CurrentAccount, src => src.Adapt<CurrentAccountDTO>());

        config.NewConfig<Account, AccountDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Holder, src => src.Holder)
                .Map(dest => dest.Number, src => src.Number)
                .Map(dest => dest.AccountType, src => src.Type)
                .Map(dest => dest.Currency, src => src.Currency)
                .Map(dest => dest.Customer, src => src.Customer)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted);
                //.Map(dest => dest.SavingAccount, src => src.SavingAccount)
                //.Map(dest => dest.CurrentAccount, src => src.CurrentAccount);
    }

}
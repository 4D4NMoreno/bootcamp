using Core.Constants;
using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class CreditCardMappingConfiguration
{
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateCreditCardModel, CreditCard>()
                .Map(dest => dest.Designation, src => src.Designation)
                .Map(dest => dest.IssueDate, src => src.IssueDate)
                .Map(dest => dest.ExpirationDate, src => src.ExpirationDate)
                .Map(dest => dest.CardNumber, src => src.CardNumber)
                .Map(dest => dest.CVV, src => src.CVV)
                .Map(dest => dest.CreditLimit, src => src.CreditLimit)
                .Map(dest => dest.AvaibleCredit, src => src.AvaibleCredit)
                .Map(dest => dest.CurrentDebt, src => src.CurrentDebt)
                .Map(dest => dest.InterestRate, src => src.InterestRate)
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.CurrencyId, src => src.CurrencyId)
                .Map(dest => dest.CreditCardStatus, src => Enum.Parse<CreditCardStatus>(src.CreditCardStatus));

            config.NewConfig<CreditCard, CreditCardDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Designation, src => src.Designation)
                .Map(dest => dest.IssueDate, src => src.IssueDate)
                .Map(dest => dest.ExpirationDate, src => src.ExpirationDate)
                .Map(dest => dest.CardNumber, src => src.CardNumber)
                .Map(dest => dest.CVV, src => src.CVV)
                .Map(dest => dest.CreditLimit, src => src.CreditLimit)
                .Map(dest => dest.AvaibleCredit, src => src.AvaibleCredit)
                .Map(dest => dest.CurrentDebt, src => src.CurrentDebt)
                .Map(dest => dest.InterestRate, src => src.InterestRate)
                .Map(dest => dest.Customer, src => src.Customer)
                .Map(dest => dest.Currency, src => src.Currency)
                .Map(dest => dest.CreditCardStatus, src => src.CreditCardStatus);
        }
    
}

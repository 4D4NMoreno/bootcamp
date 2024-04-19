using Core.Models;
using Core.Entities;
using Mapster;
using Core.Request;

namespace Infrastructure.Mappings
{
    public class TransactionMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Mapeo de TransferRequest a Transaction
            config.NewConfig<TransferRequest, Transaction>()
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.DestinationAccountId, src => src.DestinationAccountId)
                .Map(dest => dest.OriginAccountId, src => src.OriginAccountId)
                .Map(dest => dest.DestinationAccountNumber, src => src.DestinationAccountNumber)
                .Map(dest => dest.DestinationDocumentNumber, src => src.DestinationDocumentNumber)
                .Map(dest => dest.CurrencyId, src => src.CurrencyId);

            // Mapeo de Transaction a TransactionDTO
            config.NewConfig<Transaction, TransactionDTO>()
                .Map(dest => dest.Id, src => src.Id)
                //.Map(dest => dest.Account, src => src.Account)
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.OriginAccountId, src => src.OriginAccountId)
                .Map(dest => dest.DestinationAccountId, src => src.DestinationAccountId)
                .Map(dest => dest.TransactionDateTime, src => src.TransactionDateTime)
                .Map(dest => dest.DestinationAccountNumber, src => src.DestinationAccountNumber)
                .Map(dest => dest.DestinationDocumentNumber, src => src.DestinationDocumentNumber)
                .Map(dest => dest.Currency, src => src.Currency);
        }
    }
}

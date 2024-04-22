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
            config.NewConfig<TransferRequest, Transaction>()
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.TransactionDateTime, src => src.TransactionDateTime)
                .Map(dest => dest.DestinationAccountId, src => src.DestinationAccountId)
                .Map(dest => dest.OriginAccountId, src => src.OriginAccountId)
                
                .Map(dest => dest.DestinationAccountNumber, src =>
                src.DestinationAccountNumber != null
                ? src.DestinationAccountNumber
                : null)
                .Map(dest => dest.DestinationDocumentNumber, src =>
                src.DestinationDocumentNumber != null
                ? src.DestinationDocumentNumber
                : null);

            config.NewConfig<PaymentRequest, Transaction>()
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.TransactionDateTime, src => src.TransactionDateTime)
                .Map(dest => dest.OriginAccountId, src => src.OriginAccountId)
                .Map(dest => dest.DocumentNumber, src => src.DocumentNumber);


            config.NewConfig<DepositRequest, Transaction>()
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.Bank, src => src.BankId)
                .Map(dest => dest.TransactionDateTime, src => src.TransactionDateTime)
                .Map(dest => dest.DestinationAccountId, src => src.DestinationAccountId);

            config.NewConfig<WithdrawalRequest, Transaction>()
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.Bank, src => src.BankId)
                .Map(dest => dest.TransactionDateTime, src => src.TransactionDateTime)
                .Map(dest => dest.OriginAccountId, src => src.OriginAccountId);

        }
    }
}

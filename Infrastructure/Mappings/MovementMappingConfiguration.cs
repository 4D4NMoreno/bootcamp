using Core.Constants;
using Core.Entities;
using Core.Request;
using Mapster;
using System.Security.Cryptography.Xml;

namespace Infrastructure.Mappings;

public class MovementMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<TransferRequest, Movement>()
        .Map(dest => dest.Amount, src => src.Amount)
        .Map(dest => dest.TransferredDateTime, src =>TimeZoneInfo
        .ConvertTimeFromUtc(src.TransactionDateTime, TimeZoneInfo.Local))
        .Map(dest => dest.TransferStatus, src => TransferStatus.Done)
        .Map(dest => dest.MovementType, src => MovementType.Transfer);

        config.NewConfig<PaymentRequest, Movement>()
        .Map(dest => dest.AccountId, src => src.OriginAccountId)
        .Map(dest => dest.Amount, src => src.Amount)
        .Map(dest => dest.TransferredDateTime, src => TimeZoneInfo
        .ConvertTimeFromUtc(src.TransactionDateTime, TimeZoneInfo.Local))
        .Map(dest => dest.TransferStatus, src => TransferStatus.Done)
        .Map(dest => dest.MovementType, src => MovementType.PaymentsForServices);

        config.NewConfig<DepositRequest, Movement>()
        .Map(dest => dest.AccountId, src => src.DestinationAccountId)
        .Map(dest => dest.Amount, src => src.Amount)
        .Map(dest => dest.TransferredDateTime, src => TimeZoneInfo
        .ConvertTimeFromUtc(src.TransactionDateTime, TimeZoneInfo.Local))
        .Map(dest => dest.TransferStatus, src => TransferStatus.Done)
        .Map(dest => dest.MovementType, src => MovementType.Deposit);

    }

}

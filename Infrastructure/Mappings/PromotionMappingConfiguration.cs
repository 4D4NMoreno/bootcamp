using Core.Entities;
using Core.Models;
using Core.Request;
using Core.Requests;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Mappings
{
    public class PromotionMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // De la solicitud de creación a la entidad
            config.NewConfig<CreatePromotionModel, Promotion>()
    .Map(dest => dest.Name, src => src.Name)
    .Map(dest => dest.StartOfPromotion, src => src.StartOfPromotion)
    .Map(dest => dest.EndOfPromotion, src => src.EndOfPromotion)
    .Map(dest => dest.PercentageOff, src => src.PercentageOff);
            // De la entidad al DTO
            config.NewConfig<Promotion, PromotionDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.StartOfPromotion, src => src.StartOfPromotion)
                .Map(dest => dest.EndOfPromotion, src => src.EndOfPromotion)
                .Map(dest => dest.PercentageOff, src => src.PercentageOff);
              
        }
    }
}

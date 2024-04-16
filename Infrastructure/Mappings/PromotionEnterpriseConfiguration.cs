using Core.Entities;
using Core.Models;
using Core.Requests;
using Mapster;

namespace Infrastructure.Mappings
{
    public class PromotionEnterpriseMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<PromotionEnterprise, PromotionEnterpriseDTO>()
            //    .Map(dest => dest.PromotionId, src => src.PromotionId)
            //    .Map(dest => dest.EnterpriseId, src => src.EnterpriseId);
        }
    }
}

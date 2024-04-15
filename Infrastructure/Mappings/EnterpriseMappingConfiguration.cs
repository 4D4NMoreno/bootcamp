using Core.Entities;
using Core.Models;
using Core.Request;
using Core.Requests;
using Mapster;

namespace Infrastructure.Mappings
{
    public class EnterpriseMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // De la solicitud de creación a la entidad
            config.NewConfig<CreateEnterpriseModel, Enterprise>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.Mail, src => src.Mail);


            // De la entidad al DTO
            config.NewConfig<Enterprise, EnterpriseDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.Mail, src => src.Mail);
                
        }
    }
}
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IEnterpriseService
{
    Task<EnterpriseDTO> Add(CreateEnterpriseModel model);

    Task<List<EnterpriseDTO>> GetAll();

    Task<EnterpriseDTO> Update(UpdateEnterpriseModel model);
}

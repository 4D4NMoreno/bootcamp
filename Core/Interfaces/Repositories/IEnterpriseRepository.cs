using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IEnterpriseRepository
{
    Task<EnterpriseDTO> Add(CreateEnterpriseModel model);

}

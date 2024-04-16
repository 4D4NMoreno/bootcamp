using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;

namespace Infrastructure.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _repository;

    public EnterpriseService(IEnterpriseRepository repository)
    {
        _repository = repository;
    }
    public async Task<EnterpriseDTO> Add(CreateEnterpriseModel model)
    {
        return await _repository.Add(model);
    }
    public async Task<List<EnterpriseDTO>> GetAll()
    {
        return await _repository.GetAll();
    }
}

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _repository;

    public PromotionService(IPromotionRepository repository)
    {
        _repository = repository;
    }
    public async Task<PromotionDTO> Add(CreatePromotionModel model)
    {

        return await _repository.Add(model);
    }
    public async Task<PromotionDTO> Update(UpdatePromotionModel model)
    {

        return await _repository.Update(model);
    }
    public async Task<bool> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<PromotionDTO>> GetAll()
    {
        return await _repository.GetAll();
    }
}

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;

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
}

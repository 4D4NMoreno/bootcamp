using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IPromotionRepository
{
    Task<PromotionDTO> Add(CreatePromotionModel model);
}

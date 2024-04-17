using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IPromotionService
{
    Task<PromotionDTO> Add(CreatePromotionModel model);

    Task<PromotionDTO> Update(UpdatePromotionModel model);

    Task<bool> Delete(int id);

    Task<List<PromotionDTO>> GetAll();

    Task<PromotionDTO> GetById(int id);

}

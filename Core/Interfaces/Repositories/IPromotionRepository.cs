using Core.Exceptions;
using Core.Models;
using Core.Request;
using System.Collections.Generic;

namespace Core.Interfaces.Repositories;

public interface IPromotionRepository
{
    Task<PromotionDTO> Add(CreatePromotionModel model);

    Task<PromotionDTO> Update(UpdatePromotionModel model);

    Task<bool> Delete(int id);

    Task<List<PromotionDTO>> GetAll();

    Task<PromotionDTO> GetById(int id);
    
}

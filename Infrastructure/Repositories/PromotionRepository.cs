using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PromotionRepository : IPromotionRepository
{
    private readonly BootcampContext _context;

    public PromotionRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<PromotionDTO> Add(CreatePromotionModel model)
    {
        var promotion = model.Adapt<Promotion>();

        // Agrega la promoción a la base de datos
        _context.Promotions.Add(promotion);
        await _context.SaveChangesAsync();

        // Asocia la promoción a cada empresa especificada en la lista de IDs
        foreach (var enterpriseId in model.EnterpriseIds)
        {
            var enterprise = await _context.Enterprises.FindAsync(enterpriseId);
            if (enterprise != null)
            {
                enterprise.Promotions.Add(promotion);
            }
        }

        // Guarda los cambios en la base de datos
        await _context.SaveChangesAsync();

        // Mapea la promoción a PromotionDTO y devuélvela
        return promotion.Adapt<PromotionDTO>();
    }

}

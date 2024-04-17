using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly BootcampContext _context;

    public EnterpriseRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<EnterpriseDTO> Add(CreateEnterpriseModel model)
    {

        var enterpriseToCreate = model.Adapt<Enterprise>();

        _context.Enterprises.Add(enterpriseToCreate);

        await _context.SaveChangesAsync();

        var enterpriseDTO = enterpriseToCreate.Adapt<EnterpriseDTO>();

        return enterpriseDTO;
    }

    public async Task<List<EnterpriseDTO>> GetAll()
    {
        var enterprises = await _context.Enterprises
        .Include(e => e.PromotionsEnterprises)
            .ThenInclude(pe => pe.Promotion)
        .Select(e => new EnterpriseDTO
        {
            Id = e.Id,
            Name = e.Name,
            Address = e.Address,
            Phone = e.Phone,
            Email = e.Email,
            Promotions = e.PromotionsEnterprises.Select(pe => pe.Promotion.Adapt<PromotionDTO>()).ToList()
        })
        .ToListAsync();

        return enterprises;
    }

    public async Task<EnterpriseDTO> Update(UpdateEnterpriseModel model)
    {
        var enterprise = await _context.Enterprises
            .Include(e => e.PromotionsEnterprises)
            .FirstOrDefaultAsync(e => e.Id == model.Id);

        if (enterprise == null)
        {
            throw new Exception("La empresa no fue encontrada.");
        }


        _context.Entry(enterprise).CurrentValues.SetValues(model);


        if (model.PromotionsIdsToRemove != null && model.PromotionsIdsToRemove.Any())
        {
            foreach (var promotionIdToRemove in model.PromotionsIdsToRemove)
            {
                var promotionEnterpriseToRemove = enterprise.PromotionsEnterprises.FirstOrDefault(pe => pe.PromotionId == promotionIdToRemove);
                if (promotionEnterpriseToRemove != null)
                {
                    _context.Remove(promotionEnterpriseToRemove);
                }
            }
        }

        if (model.PromotionsIdsToAdd != null && model.PromotionsIdsToAdd.Any())
        {
            foreach (var promotionIdToAdd in model.PromotionsIdsToAdd)
            {

                if (await _context.Promotions.AnyAsync(p => p.Id == promotionIdToAdd))
                {

                    if (!enterprise.PromotionsEnterprises.Any(pe => pe.PromotionId == promotionIdToAdd))
                    {
                        var promotionEnterpriseToAdd = new PromotionEnterprise
                        {
                            PromotionId = promotionIdToAdd,
                            EnterpriseId = enterprise.Id
                        };
                        _context.Add(promotionEnterpriseToAdd);


                    }
                    
                }
                else
                {
                    throw new Exception($"La promoción con el ID {promotionIdToAdd} no fue encontrada.");
                }
            }
        }


        await _context.SaveChangesAsync();

        var enterpriseDTO = enterprise.Adapt<EnterpriseDTO>();
        return enterpriseDTO;
    }




}


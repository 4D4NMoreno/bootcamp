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
}

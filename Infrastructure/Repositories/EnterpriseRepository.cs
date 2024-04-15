using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;

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

        var enterprise = model.Adapt<Enterprise>();

        _context.Enterprises.Add(enterprise);

        await _context.SaveChangesAsync();
       // await _context.Entry(enterprise)
       //.Collection(e => e.Promotions)
       //.LoadAsync();

        return enterprise.Adapt<EnterpriseDTO>();
    }
}

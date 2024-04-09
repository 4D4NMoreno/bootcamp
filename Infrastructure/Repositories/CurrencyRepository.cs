using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly BootcampContext _context;

    public CurrencyRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<CurrencyDTO> Add(CreateCurrencyModel model)
    {


        var currencyToCreate = model.Adapt<Currency>();

        _context.Currencies.Add(currencyToCreate);

        await _context.SaveChangesAsync();

        var currencyDTO = currencyToCreate.Adapt<CurrencyDTO>();

        return currencyDTO;
    }
}

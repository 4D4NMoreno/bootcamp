using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    private readonly BootcampContext _context;

    public CreditCardRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<CreditCardDTO> Add(CreateCreditCardModel model)
    {
        var query = _context.CreditCards
                .Include(c => c.Currency)
                .AsQueryable();
           


        var creditCardToCreate = model.Adapt<CreditCard>();

        var creditCardCurrency = await _context.Currencies.FindAsync(creditCardToCreate.CurrencyId);

        var creditCardCustomer = await _context.Customers
       .Include(c => c.Bank)
       .FirstOrDefaultAsync(c => c.Id == model.CustomerId);


        _context.CreditCards.Add(creditCardToCreate);

        await _context.SaveChangesAsync();

        var creditCardDTO = creditCardToCreate.Adapt<CreditCardDTO>();

        return creditCardDTO;
    }

    public async Task<bool> BeValidCurrencyId(int currencyId)
    {
        return await _context.Currencies.AnyAsync(cc => cc.Id == currencyId);
    }

    public async Task<bool> BeValidCustomerId(int customerId)
    {
 
        return await _context.Customers.AnyAsync(c => c.Id == customerId);

    }

    public async Task<List<CreditCardDTO>> GetAll()
    {
        var creditCards = await _context.CreditCards
            .Include(c => c.Customer).ThenInclude(x => x.Bank)
            .Include(c => c.Currency)
            .ToListAsync();

        var creditCardDTOs = creditCards.Select(cc => cc.Adapt<CreditCardDTO>()).ToList();

        return creditCardDTOs;
    }
    public async Task<CreditCardDTO> Update(UpdateCreditCardModel model)
    {
        //var creditCards = await _context.CreditCards
        //    .Include(c => c.Customer).ThenInclude(x => x.Bank)
        //    .Include(c => c.Currency)
        //    .ToListAsync();
        var creditCard = await _context.CreditCards.FindAsync(model.Id);
        var customer = await _context.Customers.FindAsync(model.CustomerId);
        var currency = await _context.Currencies.FindAsync(model.CurrencyId);

        if (creditCard is null) throw new Exception("Customer was not found");

        model.Adapt(creditCard);

        _context.CreditCards.Update(creditCard);

        await _context.SaveChangesAsync();

        var creditCardDTO = creditCard.Adapt<CreditCardDTO>();

        return creditCardDTO;
    }

}

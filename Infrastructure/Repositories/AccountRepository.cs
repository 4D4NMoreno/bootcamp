using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BootcampContext _context;

    public AccountRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<AccountDTO> Add(CreateAccountRequest request)
    {
        var currency = await _context.Currencies.FindAsync(request.CurrencyId);

        if (currency is null)
        {
            throw new BusinessLogicException("Currency not found");
        }

        var customer = await _context.Customers.FindAsync(request.CustomerId);

        if (customer is null)
        {
            throw new BusinessLogicException("Customer not found");
        }

        var account = request.Adapt<Account>();

        if (account.Type == AccountType.Saving)
        {
            account.SavingAccount = request.CreateSavingAccount.Adapt<SavingAccount>();
        }

        if (account.Type == AccountType.Current)
        {
            account.CurrentAccount = request.CreateCurrentAccount.Adapt<CurrentAccount>();
        }

        _context.Accounts.Add(account);

        await _context.SaveChangesAsync();

        var createdAccount = await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.SavingAccount)
            .Include(a => a.CurrentAccount)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(a => a.Id == account.Id);

        return createdAccount.Adapt<AccountDTO>();
    }

    public async Task<AccountDTO> GetById(int id)
    {
        var account = await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.Customer)
            .Include(a => a.SavingAccount)
            .Include(a => a.CurrentAccount)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (account is null) throw new NotFoundException($"The account with id: {id} doest not exist");

        return account.Adapt<AccountDTO>();
    }


    public async Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter)
    {
        var query = _context.Accounts
                .Include(c => c.Currency)
                .AsQueryable();

        if (filter.Number is not null)
        {
            query = query.Where(x =>
                x.Number == filter.Number);
        }
        if (filter.Type is not null)
        {
            query = query.Where(x => x.Type == filter.Type);

        }
        if (filter.CurrencyId is not null)
        {
            query = query.Where(x => x.CurrencyId == filter.CurrencyId);
        }

        var result = await query.ToListAsync();


        var accountDTOs = result.Adapt<List<AccountDTO>>();

        return accountDTOs;
    }
}

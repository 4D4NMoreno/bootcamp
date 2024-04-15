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
            .Include(a => a.Customer)
            .Include(a => a.SavingAccount)
            .Include(a => a.CurrentAccount)
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

        if (filter.Number != null)
        {
            query = query.Where(x =>
                x.Number == filter.Number);
        }
        if (filter.CurrencyId != null)
        {
            query = query.Where(x => x.Type == filter.Type);

        }
        if (filter.CurrencyId != null)
        {
            query = query.Where(x => x.CurrencyId == filter.CurrencyId);
        }
        var result = await query.ToListAsync();


        var accountDTOs = result.Adapt<List<AccountDTO>>();

        return accountDTOs;
    }
}
//    public async Task<AccountDTO> Add(CreateAccountModel model)
//    {


//        var accountToCreate = model.Adapt<Account>();

//        var accountCurrency = await _context.Currencies.FindAsync(accountToCreate.CurrencyId);
//        if (accountCurrency == null)
//        {
//            throw new Exception("Currency not found.");
//        }
//        accountToCreate.Currency = accountCurrency;


//        var accountCustomer = await _context.Customers
//            .Include(c => c.Bank)
//            .FirstOrDefaultAsync(c => c.Id == model.CustomerId);
//        if (accountCustomer == null)
//        {
//            throw new Exception("Customer not found.");
//        }

//        if (model.AccountType == "Saving")
//            if (model.AccountType == "Saving")
//            {
//                // Verifica si hay una instancia de SavingAccountDTO existente
//                if (accountToCreate.SavingAccount != null)
//                {
//                    // Crear una nueva instancia de SavingAccount y asignar sus propiedades
//                    var savingAccount = new SavingAccount
//                    {
//                        Id = accountToCreate.SavingAccount.Id, // Asigna el Id
//                        SavingType = accountToCreate.SavingAccount.SavingType, // Asigna el SavingType
//                        HolderName = accountToCreate.SavingAccount.HolderName // Asigna el HolderName
//                                                                              // Asigna otras propiedades según sea necesario
//                    };

//                    // Asigna la instancia de SavingAccount a accountToCreate.SavingAccount
//                    accountToCreate.SavingAccount = savingAccount;
//                }
//                else
//                {
//                    // Si accountToCreate.SavingAccount es null, crea una nueva instancia de SavingAccount
//                    accountToCreate.SavingAccount = new SavingAccount();
//                }
//            }
//            else

//        if (model.AccountType == "Current")
//            {
//                // Verifica si hay una instancia de CurrentAccountDTO existente
//                if (accountToCreate.CurrentAccount != null)
//                {
//                    // Crear una nueva instancia de CurrentAccount y asignar sus propiedades
//                    var currentAccount = new CurrentAccount
//                    {
//                        Id = accountToCreate.CurrentAccount.Id, // Asigna el Id
//                        OperationalLimit = accountToCreate.CurrentAccount.OperationalLimit, // Asigna el OperationalLimit
//                        MonthAverage = accountToCreate.CurrentAccount.MonthAverage, // Asigna el MonthAverage
//                        Interest = accountToCreate.CurrentAccount.Interest, // Asigna el Interest
//                        AccountId = accountToCreate.CurrentAccount.AccountId // Asigna el AccountId
//                                                                             // Asigna otras propiedades según sea necesario
//                    };

//                    // Asigna la instancia de CurrentAccount a accountToCreate.CurrentAccount
//                    accountToCreate.CurrentAccount = currentAccount;
//                }
//                else
//                {
//                    // Si accountToCreate.CurrentAccount es null, crea una nueva instancia de CurrentAccount
//                    accountToCreate.CurrentAccount = new CurrentAccount();
//                }



//            }
//        _context.Accounts.Add(accountToCreate);

//        await _context.SaveChangesAsync();


//        var accountDTO = accountToCreate.Adapt<AccountDTO>();

//        return accountDTO;
//    }

//}
//public async Task<AccountDTO> Add(CreateAccountModel model)
//{
//    {
//        var query = _context.Accounts
//                .Include(c => c.Currency)
//                .AsQueryable();



//        var AccountToCreate = model.Adapt<Account>();
//        var AccountCurrency = await _context.Currencies.FindAsync(AccountToCreate.CurrencyId);

//        var AccountCustomer = await _context.Customers
//       .Include(c => c.Bank)
//       .FirstOrDefaultAsync(c => c.Id == model.CustomerId);

//        _context.Accounts.Add(AccountToCreate);

//        await _context.SaveChangesAsync();

//        var accountDTO = AccountToCreate.Adapt<AccountDTO>();

//        return accountDTO;
//    }




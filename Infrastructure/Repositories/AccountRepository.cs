using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BootcampContext _context;

    public AccountRepository(BootcampContext context)
    {
        _context = context;

    }
    public async Task<AccountDTO> Add(CreateAccountModel model)
    {
        var customer = await _context.Accounts.FindAsync(model.CustomerId);
        var currency = await _context.Accounts.FindAsync(model.CurrencyId);
        var customer2 = await _context.Customers
        .Include(c => c.Bank)
        .FirstOrDefaultAsync(c => c.Id == model.CustomerId);

        var query = _context.Accounts
       .Include(c => c.SavingAccount)
       .Include(c => c.Customer)
       .Include(c => c.Currency)
       .AsQueryable();
        var result = await query.ToListAsync();

        var accountToCreate = model.Adapt<Account>();
        _context.Accounts.Add(accountToCreate);
        await _context.SaveChangesAsync();


        if (model.AccountType == "Saving")
        {
            var savingAccountDTO = new SavingAccountDTO
            {
                AccountId = accountToCreate.Id,
                HolderName = accountToCreate.Holder,
                //SavingType = model.SavingType
            };
            var savingAccount = savingAccountDTO.Adapt<SavingAccount>();
            _context.SavingAccounts.Add(savingAccount);
        }

        else if (model.AccountType == "Current")
        {
            var currentAccountDTO = new CurrentAccountDTO
            {
                AccountId = accountToCreate.Id,
                OperationalLimit = model.currentAccount.OperationalLimit,
                MonthAverage = model.currentAccount.MonthAverage,
                Interest = model.currentAccount.Interest

            };
            var currentAccount = currentAccountDTO.Adapt<CurrentAccount>();
            _context.CurrentAccounts.Add(currentAccount);
        }
        else
        {
            throw new Exception("Invalid account type");
        }

        await _context.SaveChangesAsync();

        var accountDTO = accountToCreate.Adapt<AccountDTO>();
        return accountDTO;
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




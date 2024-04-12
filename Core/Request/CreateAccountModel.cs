using Core.Constants;
using Core.Entities;
using Core.Models;
using System.Diagnostics;

namespace Core.Request;

public class CreateAccountModel
{
    public string Holder { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string AccountType { get; set; }

    public int CurrencyId { get; set; }

    public int CustomerId { get; set; }

    public SavingAccountDTO savingAccount { get; set; }

    public CurrentAccountDTO currentAccount { get; set; }

}

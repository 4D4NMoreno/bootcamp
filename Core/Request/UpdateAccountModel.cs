using Core.Constants;
using Core.Models;
using Core.Requests;

namespace Core.Request;

public class UpdateAccountModel
{
    public int Id { get; set; }
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public int CurrencyId { get; set; }
    public int CustomerId { get; set; }
    public AccountType Type { get; set; } = AccountType.Current;

    public CreateSavingAccount? SavingAccount { get; set; }
    public CreateCurrentAccount? CurrentAccount { get; set; }


}

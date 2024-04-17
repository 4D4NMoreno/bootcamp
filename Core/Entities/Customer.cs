using Core.Constants;

namespace Core.Entities;

public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Lastname { get; set; }

    public string DocumentNumber { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? Mail { get; set; }

    public string? Phone { get; set; }

    public DateTime? Birth { get; set; }

    public int BankId { get; set; }

    public int CreditCardId { get; set; }

    public ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    public CustomerStatus CustomerStatus { get; set; } = CustomerStatus.Active;
    public virtual Bank Bank { get; set; } = null!;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();

    public ICollection<Product> Products { get; set; } = new List<Product>();

}

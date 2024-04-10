using Core.Constants;

namespace Core.Entities;

public class CreditCard
{
    public int Id { get; set; }

    public string? Designation { get; set; }

    public DateTime IssueDate { get; set; } 

    public DateTime ExpirationDate { get; set; }

    public int CardNumber { get; set; }

    public int CVV { get; set; }

    public decimal CreditLimit { get; set; }

    public decimal AvaibleCredit { get; set; }

    public decimal CurrentDebt {  get; set; } 

    public decimal InterestRate { get; set;}

    public int CustomerId { get; set; }

    public int CurrencyId { get; set; }

    public CreditCardStatus CreditCardStatus { get; set; } = CreditCardStatus.Enabled;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;

}

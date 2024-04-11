namespace Core.Request;

public class UpdateCreditCardModel
{
    public int Id { get; set; }
    public string? Designation { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int CardNumber { get; set; }

    public int CVV { get; set; }

    public decimal CreditLimit { get; set; }

    public decimal AvaibleCredit { get; set; }

    public decimal CurrentDebt { get; set; }

    public decimal InterestRate { get; set; }

    public int CustomerId { get; set; }

    public int CurrencyId { get; set; }

    public string? CreditCardStatus { get; set; }

}

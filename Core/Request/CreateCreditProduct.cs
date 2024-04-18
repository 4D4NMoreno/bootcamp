namespace Core.Request;

public class CreateCreditProduct
{
    public decimal Amount { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }
    public int? Term { get; set; }

    public int CustomerId { get; set; }

    public int CurrencyId { get; set; }

    public int BankId { get; set; }

}

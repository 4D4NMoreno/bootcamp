namespace Core.Request;

public class CreateCreditProduct
{
    public decimal Amount { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }
    public int? Term { get; set; }

}

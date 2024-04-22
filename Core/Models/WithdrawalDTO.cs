namespace Core.Models;

public class WithdrawalDTO
{
    public string MovementType { get; set; }
    public string OriginAccount { get; set; }
    public string Bank { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDateTime { get; set; }
}

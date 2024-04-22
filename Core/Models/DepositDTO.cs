namespace Core.Models;

public class DepositDTO
{
    public string MovementType { get; set; }
    public string DestinationAccount { get; set; }
    public string Bank { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDateTime { get; set; }
}

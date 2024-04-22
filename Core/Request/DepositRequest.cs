namespace Core.Request;

public class DepositRequest
{
    public int DestinationAccountId { get; set; }
    public int BankId { get; set; }
    public decimal Amount { get; set;}
    public DateTime TransactionDateTime { get; set; }
}

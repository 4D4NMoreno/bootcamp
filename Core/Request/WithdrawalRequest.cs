namespace Core.Request;

public class WithdrawalRequest
{
    public int OriginAccountId { get; set; }
    public int BankId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDateTime { get; set; }
}

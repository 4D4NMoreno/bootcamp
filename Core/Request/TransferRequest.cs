namespace Core.Request;

public class TransferRequest
{
    public int? DestinationBank { get; set; }
    public int OriginAccountId { get; set; }
    public int? DestinationAccountId { get; set; }
    public string? DestinationAccountNumber { get; set; } 
    public string? DestinationDocumentNumber { get; set; } 
    public decimal Amount { get; set; }
    public DateTime TransactionDateTime { get; set; }
}

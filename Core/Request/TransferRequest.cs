namespace Core.Request;

public class TransferRequest
{
    public string DestinationBank { get; set; }

    public int OriginAccountId { get; set; }
    public int DestinationAccountId { get; set; }

    public string DestinationAccountNumber { get; set; } 
    public string DestinationDocumentNumber { get; set; } 
    public int CurrencyId { get; set; } 
    public decimal Amount { get; set; } 
}

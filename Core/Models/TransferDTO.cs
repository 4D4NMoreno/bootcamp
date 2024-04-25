using Core.Constants;

namespace Core.Models;

public class TransferDTO
{
    public string MovementType { get; set; }
    public string? Destination { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime TransferredDateTime { get; set; }
    public string TransferStatus { get; set; }

}

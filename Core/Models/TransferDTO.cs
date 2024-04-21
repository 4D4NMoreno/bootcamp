using Core.Constants;

namespace Core.Models;

public class TransferDTO
{
    public int Id { get; set; }
    public string MovementType { get; set; }
    public string? Destination { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime TransferredDateTime { get; set; }
    public string TransferStatus { get; set; }
    public int AccountId { get; set; }
}

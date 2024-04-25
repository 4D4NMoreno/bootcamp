using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class TransactionDTO
{
    public int Id { get; set; }
    public int OriginAccountId { get; set; }
    public int? DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public string? Bank { get; set; }
    public string? DestinationAccountNumber { get; set; }
    public string? DestinationDocumentNumber { get; set; }
    public string? Description { get; set; }
}

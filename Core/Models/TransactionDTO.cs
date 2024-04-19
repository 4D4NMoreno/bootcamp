using Core.Entities;

namespace Core.Models;

public class TransactionDTO
{
    public int Id { get; set; }
    public int OriginAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public string? DestinationBank { get; set; }
    public string? DestinationAccountNumber { get; set; }
    public string? DestinationDocumentNumber { get; set; }
    public int CurrencyId { get; set; }

    public int AccountId { get; set; }
    public CurrencyDTO? Currency { get; set; }
    public AccountDTO? Account { get; set; }
}

namespace Core.Models;

public class CreditProductDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }
    public int? Term { get; set; }
    public BankDTO Bank { get; set; }
    public CurrencyDTO Currency { get; set; } = null!;
    public CustomerDTO Customer { get; set; } = null!;
}

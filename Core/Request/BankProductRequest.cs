using Core.Constants;

namespace Core.Request;

public class BankProductRequest
{
    public ProductName ProductName { get; set; }
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
    public string? Description { get; set; }

    public DateTime ApplicationDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
}


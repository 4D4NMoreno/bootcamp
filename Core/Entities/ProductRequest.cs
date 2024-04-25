
using Core.Constants;

namespace Core.Entities;

public class ProductRequest
{
    public int Id { get; set; }
    public ProductName ProductName { get; set; } = ProductName.Credit;
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime ApprovalDate { get; set; }
    public string? Description { get; set; }

}

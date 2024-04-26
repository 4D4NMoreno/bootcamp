
using Core.Constants;

namespace Core.Entities;

public class ProductRequest
{
    public int Id { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime ApprovalDate { get; set; }
    public string? Description { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }

}

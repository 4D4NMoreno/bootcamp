using Core.Constants;
using Core.Request;

namespace Core.Models;

public class ProductRequestDTO
{
    public int Id { get; set; }
    public ProductName ProductName { get; set; } = ProductName.CreditCard;
    public string Description { get; set; } 

    public CurrencyDTO Currency { get; set; } = null!;
    public CustomerDTO Customer { get; set; } = null!;

    public DateTime ApplicationDate { get; set; }
    public DateTime ApprovalDate { get; set; }

}

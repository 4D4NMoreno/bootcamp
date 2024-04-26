using Core.Constants;

namespace Core.Request;

public class CreateProductRequest

{
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
    public string? Description { get; set; }
    public DateTime ApplicationDate { get; set; }

}


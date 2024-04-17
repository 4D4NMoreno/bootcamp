using Core.Constants;
using Core.Request;

namespace Core.Models;

public class ProductDTO
{
    public int Id { get; set; }
    public ProductType ProductType { get; set; }
    public CreateCreditProduct CreditProduct { get; set; }
}

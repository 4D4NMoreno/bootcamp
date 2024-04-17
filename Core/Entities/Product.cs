
using Core.Constants;

namespace Core.Entities;

public class Product
{
    public ProductType ProductType { get; set; } = ProductType.Credit;
    public int CreditProductId { get; set; }
    public int CreditCardProductId { get; set; }
    public int CurrentAccountProductId { get; set; }
    public CreditProduct CreditProduct { get; set; }

    public CreditCardProduct CreditCardProduct { get; set; }

    public CurrentAccountProduct CurrentAccountProduct { get; set; }


}


using Core.Constants;

namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public ProductType ProductType { get; set; } = ProductType.Credit;

    public int BankId { get; set; }
    public Bank Bank { get; set; }


    public int CustomerId { get; set; }
    public Customer Customer { get; set; }


    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }


    public CreditProduct CreditProduct { get; set; }

    public CreditCardProduct CreditCardProduct { get; set; }

    public CurrentAccountProduct CurrentAccountProduct { get; set; }


}

namespace Core.Entities;

public class CreditCardProduct
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }


    public int BankId { get; set; }
    public Bank Bank { get; set; }


    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }

    public ICollection<Product> products { get; set; } = new List<Product>();

}



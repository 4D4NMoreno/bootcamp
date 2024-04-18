namespace Core.Entities;

public class CreditCardProduct
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }

    public int ProductId { get; set; }
    public Product product { get; set; }

}



using Core.Entities;

namespace Core.Request;

public class CreateCreditCardProduct
{
    public string Brand { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }

    public int BankId { get; set; }



    public int CustomerId { get; set; }


    public int CurrencyId { get; set; }

}

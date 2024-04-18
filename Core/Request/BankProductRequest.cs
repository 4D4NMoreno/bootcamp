using Core.Constants;

namespace Core.Request;

public class BankProductRequest
{
    public ProductType ProductType { get; set; }
    public CreateCreditProduct CreateCreditProduct { get; set;}
    public CreateCreditCardProduct CreateCreditCardProduct { get; set; }
    public CreateCurrentAccountProduct CreateCurrentAccountProduct { get; set; }

}


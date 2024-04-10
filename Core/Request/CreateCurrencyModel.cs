namespace Core.Requests;

public class CreateCurrencyModel 
{ 
    public string Name { get; set; }
    public decimal BuyValue { get; set; }
    public decimal SellValue { get; set; }
}

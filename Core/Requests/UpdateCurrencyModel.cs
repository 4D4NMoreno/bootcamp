namespace Core.Request;

public class UpdateCurrencyModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal BuyValue { get; set; }

    public decimal SellValue { get; set; }


}


namespace Core.Request;

public class FilterTransactionModel
{
    public int AccountId { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }  
    public string? Description { get; set; }
}

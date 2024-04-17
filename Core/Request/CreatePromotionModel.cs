namespace Core.Request;

public class CreatePromotionModel
{

    public string? Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int  Discount { get; set; }
    public List<int> EnterpriseIds { get; set; } = new List<int>();
}

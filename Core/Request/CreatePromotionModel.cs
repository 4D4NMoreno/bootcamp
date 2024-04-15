namespace Core.Request;

public class CreatePromotionModel
{

    public string Name { get; set; }
    public DateTime StartOfPromotion { get; set; }
    public DateTime EndOfPromotion { get; set; }
    public int PercentageOff { get; set; }
    public List<int> EnterpriseIds { get; set; }
}

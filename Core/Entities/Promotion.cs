using Core.Constants;

namespace Core.Entities;

public class Promotion
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime StartOfPromotion { get; set; }

    public DateTime EndOfPromotion { get; set; }

    public int PercentageOff { get; set; }

    public PromotionStatus PromotionStatus { get; set; } = PromotionStatus.Active;

    public int EnterpriseId { get; set; }

    public Enterprise Enterprise { get; set; }

    //public ICollection<Enterprise> Enterprises { get; set; } = new List<Enterprise>();


}

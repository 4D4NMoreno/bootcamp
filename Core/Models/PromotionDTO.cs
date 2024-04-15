using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class PromotionDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime StartOfPromotion { get; set; }

    public DateTime EndOfPromotion { get; set; }

    public int PercentageOff { get; set; }

    public PromotionStatus PromotionStatus { get; set; } = PromotionStatus.Active;

    public EnterpriseDTO Enterprises { get; set; }

}

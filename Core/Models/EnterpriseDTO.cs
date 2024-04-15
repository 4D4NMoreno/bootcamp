using Core.Entities;

namespace Core.Models;

public class EnterpriseDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;


    public string Address { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    //public int PromotionId { get; set; }

    //public PromotionDTO Promotion { get; set; }
}

namespace Core.Entities
{
    public class Enterprise
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;


        public string Address { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Mail { get; set; }

        //public int PromotionId { get; set; }

        //public Promotion Promotion { get; set; } = null!;

        public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    }
}

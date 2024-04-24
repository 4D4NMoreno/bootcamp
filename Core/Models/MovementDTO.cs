namespace Core.Models
{
    public class MovementDTO
    {
        public int AccountId { get; set; }
        //public string MovementType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferredDateTime { get; set; }
        public string TransferStatus { get; set; }
    }
}
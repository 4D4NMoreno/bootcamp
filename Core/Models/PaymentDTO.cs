using Core.Constants;

namespace Core.Models;

public class PaymentDTO
{
    public string OriginAccount { get; set; }

    public string DocumentNumber { get; set; }

    public decimal Amount { get; set; }
    
    public string Description { get; set; }
    
    public DateTime TransactionDateTime { get; set; }
}

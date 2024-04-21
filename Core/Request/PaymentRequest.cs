using System;

namespace Core.Request
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string DocumentNumber { get; set; }
        public string Description { get; set; } 
        public int DebitedAccountId { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}

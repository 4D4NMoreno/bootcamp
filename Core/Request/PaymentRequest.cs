using System;

namespace Core.Request
{
    public class PaymentRequest
    {
        public int OriginAccountId { get; set; }
        public string DocumentNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}

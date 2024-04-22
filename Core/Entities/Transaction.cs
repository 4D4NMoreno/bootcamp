using Core.Constants;
using System;

namespace Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; } = TransactionType.Transfer;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? Bank { get; set; }
        public string? DocumentNumber { get; set; }
        public int OriginAccountId { get; set; } 
        public int? DestinationAccountId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string? DestinationAccountNumber { get; set; }
        public string? DestinationDocumentNumber { get; set; }

    }
}

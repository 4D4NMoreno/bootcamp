﻿namespace Core.Models;

public class PaymentDTO
{
    public string MovementType { get; set; }

    public string OriginAccount { get; set; }

    public string DocumentNumber { get; set; }

    public decimal Amount { get; set; }
    
    public string Description { get; set; }
    
    public DateTime TransactionDateTime { get; set; }
}

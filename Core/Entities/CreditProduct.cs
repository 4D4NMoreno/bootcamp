using Core.Entities;
using System;

public class CreditProduct
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }
    public int? Term { get; set; }

    public string ProductId { get; set; }
    public Product product { get; set; }

}

using Core.Entities;
using System;

public class CurrentAccountProduct
{
    public int Id { get; set; }
    public decimal DepositAmount { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }


    public int ProductId { get; set; }
    public Product product { get; set; }


}

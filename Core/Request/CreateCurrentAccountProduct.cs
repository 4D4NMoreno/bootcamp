using Core.Entities;

namespace Core.Request;

public class CreateCurrentAccountProduct
{
    public decimal DepositAmount { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ApprovalDate { get; set; }


    public int BankId { get; set; }


    public int CustomerId { get; set; }


    public int CurrencyId { get; set; }
}

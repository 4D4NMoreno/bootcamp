namespace Core.Interfaces.Repositories;

public interface IRequestTransactionSum
{
    public DateTime TransactionDateTime { get; set; }
    public int AccountId { get; set; }

}

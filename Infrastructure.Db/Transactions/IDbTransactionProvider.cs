namespace Infrastructure.Db.Transactions
{
    using System.Data;

    public interface IDbTransactionProvider
    {
        IDbTransaction CurrentTransaction { get; }
    }
}
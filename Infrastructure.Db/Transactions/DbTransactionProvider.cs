namespace Infrastructure.Db.Transactions
{
    using System;
    using System.Data;

    public sealed class DbTransactionProvider : IDbTransactionProvider
    {
        public IDbTransaction CurrentTransaction
        {
            get
            {
                if (DbTransactionContext.HasBindedTransaction)
                    return DbTransactionContext.CurrentTransaction;
                
                throw new InvalidOperationException("Database access logic cannot be used, if transaction not started");
            }
        }

    }
}
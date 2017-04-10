namespace Infrastructure.Db.Transactions
{
    using System;
    using System.Data;

    public class DbTransactionContext
    {
        [ThreadStatic]
        private static IDbTransaction _transaction;

        public static IDbTransaction CurrentTransaction => _transaction;

        public static bool HasBindedTransaction => CurrentTransaction != null;

        public static void Bind(IDbTransaction transaction)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public static void Unbind()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No transaction was bound");

            _transaction = null;
        }
    }
}
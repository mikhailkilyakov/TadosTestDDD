namespace Infrastructure.Db.Commands
{
    using System;
    using System.Data;
    using Transactions;

    public abstract class DbCommandFactoryBase : IDbCommandFactory
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public DbCommandFactoryBase(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }

        protected abstract IDbCommand CreateCommand();

        public IDbCommand Create()
        {
            IDbTransaction currentTransaction = _dbTransactionProvider.CurrentTransaction;
            IDbCommand command = CreateCommand();

            command.Connection = currentTransaction.Connection;
            command.Transaction = currentTransaction;

            return command;
        }
    }
}
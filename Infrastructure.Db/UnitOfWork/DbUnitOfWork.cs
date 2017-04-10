namespace Infrastructure.Db.UnitOfWork
{
    using System;
    using System.Data;
    using System.Threading;
    using Domain.UnitOfWork;
    using Transactions;

    public class DbUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public DbUnitOfWork(IDbConnection connection, IsolationLevel isolationLevel)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

            _connection = connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction(isolationLevel);

            DbTransactionContext.Bind(_transaction);
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            DbTransactionContext.Unbind();
            _transaction.Dispose();
            _transaction = null;
            _connection.Dispose();
        }
    }
}
namespace Infrastructure.Db.UnitOfWork
{
    using System.Data;
    using Connections;
    using Domain.UnitOfWork;
    public class DbUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbUnitOfWorkFactory(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new DbUnitOfWork(_connectionFactory.OpenConnection(), isolationLevel);
        }

        public IUnitOfWork Create()
        {
            return Create(IsolationLevel.ReadCommitted);
        }
    }
}
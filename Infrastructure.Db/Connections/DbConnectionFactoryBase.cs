namespace Infrastructure.Db.Connections
{
    using System;
    using System.Data;

    public abstract class DbConnectionFactoryBase : IDbConnectionFactory
    {
        private readonly string _connectionString;

        protected DbConnectionFactoryBase(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        protected abstract IDbConnection CreateConnection(string connectionString);

        public IDbConnection OpenConnection()
        {
            return CreateConnection(_connectionString);
        }
    }
}
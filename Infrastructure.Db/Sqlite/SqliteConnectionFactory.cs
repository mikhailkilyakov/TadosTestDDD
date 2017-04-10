namespace Infrastructure.Db.Sqlite
{
    using System.Data;
    using Connections;
    using Microsoft.Data.Sqlite;

    public class SqliteConnectionFactory : DbConnectionFactoryBase
    {
        public SqliteConnectionFactory(string connectionString) : base(connectionString)
        {
        }

        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
    }
}
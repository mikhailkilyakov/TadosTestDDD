namespace Infrastructure.Db.Sqlite
{
    using System.Data;
    using Commands;
    using Microsoft.Data.Sqlite;
    using Transactions;

    public class SqliteCommandFactory : DbCommandFactoryBase
    {
        public SqliteCommandFactory(IDbTransactionProvider dbTransactionProvider) : base(dbTransactionProvider)
        {
        }

        protected override IDbCommand CreateCommand()
        {
            return new SqliteCommand();
        }
    }
}
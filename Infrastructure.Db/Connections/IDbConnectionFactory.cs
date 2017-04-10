namespace Infrastructure.Db.Connections
{
    using System.Data;

    public interface IDbConnectionFactory
    {
        IDbConnection OpenConnection();
    }
}
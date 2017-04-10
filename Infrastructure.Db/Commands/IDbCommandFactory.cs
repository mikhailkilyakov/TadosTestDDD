namespace Infrastructure.Db.Commands
{
    using System.Data;

    public interface IDbCommandFactory
    {
        IDbCommand Create();
    }
}
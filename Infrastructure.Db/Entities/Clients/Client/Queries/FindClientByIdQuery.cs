namespace Infrastructure.Db.Entities.Clients.Client.Queries
{
    using Dapper;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Transactions;

    public class FindClientByIdQuery : IQuery<FindById, Client>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindClientByIdQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public Client Ask(FindById criterion)
        {
            return _dbTransactionProvider.CurrentTransaction.Connection.QueryFirstOrDefault<Client>(
                @"SELECT
                    Id,
                    Name, 
                    Inn 
                FROM Client 
                WHERE Id = @Id;", new { Id = criterion.Id });
        }
    }
}
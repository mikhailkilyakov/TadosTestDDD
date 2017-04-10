namespace Infrastructure.Db.Entities.Clients.Client.Queries
{
    using Dapper;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Transactions;

    public class FindClientByInnQuery : IQuery<FindByInn, Client>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindClientByInnQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public Client Ask(FindByInn criterion)
        {
            return _dbTransactionProvider.CurrentTransaction.Connection.QueryFirstOrDefault<Client>(
                @"SELECT
                    Id,
                    Name, 
                    Inn 
                FROM Client 
                WHERE Inn = @Inn;", new { Inn = criterion.Inn });
        }
    }
}
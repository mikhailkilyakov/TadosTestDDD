namespace Infrastructure.Db.Entities.Clients.Client.Queries
{
    using System.Collections.Generic;
    using Dapper;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Transactions;
    using WebApi.Application.Infrastructure.Queries.Criteria;

    public class FindClientsByCountAndOffsetQuery : IQuery<FindByCountAndOffset, IEnumerable<Client>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindClientsByCountAndOffsetQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public IEnumerable<Client> Ask(FindByCountAndOffset criterion)
        {
            return _dbTransactionProvider.CurrentTransaction.Connection
                .Query<Client>(@"
                    SELECT
                        Id,
                        Name,
                        Inn
                    FROM Client
                    ORDER BY Name ASC
                    LIMIT @Limit OFFSET @Offset", new { Limit = criterion.Count, Offset = criterion.Offset });
        }
    }
}
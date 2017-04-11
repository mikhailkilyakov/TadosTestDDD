namespace Infrastructure.Db.Entities.Bills.Bill.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Dapper;
    using Domain.Entities.Bills;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Transactions;
    using WebApi.Application.Infrastructure.Queries.Criteria;

    public class FindBillsByCountAndOffsetQuery : IQuery<FindByCountAndOffset, IEnumerable<Bill>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindBillsByCountAndOffsetQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public IEnumerable<Bill> Ask(FindByCountAndOffset criterion)
        {
            return _dbTransactionProvider.CurrentTransaction.Connection
                .Query<Bill, Client, Bill>(@"
                    SELECT
                        Bill.Id,
                        CAST(Bill.Sum AS REAL) `Sum`,
                        Bill.Number,
                        Bill.CreatedAt,
                        Bill.PayedAt,
                        Client.Id,
                        Client.Name,
                        Client.Inn
                    FROM Bill
                    JOIN Client ON Client.Id = Bill.ClientId
                    ORDER BY Bill.CreatedAt ASC, Bill.Number ASC
                    LIMIT @Limit OFFSET @Offset", (bill, client) =>
                {
                    bill.GetType().GetProperty("Client").SetValue(bill, client);

                    return bill;
                }, new
                {
                    Limit = criterion.Count,
                    Offset = criterion.Offset
                });
        }
    }
}
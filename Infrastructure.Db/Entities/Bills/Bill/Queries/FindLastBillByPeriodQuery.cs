namespace Infrastructure.Db.Entities.Bills.Bill.Queries
{
    using System.Linq;
    using System.Reflection;
    using Dapper;
    using Domain.Entities.Bills;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Transactions;

    public class FindLastBillByPeriodQuery : IQuery<FindLastByPeriod, Bill>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindLastBillByPeriodQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public Bill Ask(FindLastByPeriod criterion)
        {
            return _dbTransactionProvider.CurrentTransaction.Connection
                .Query<Bill, Client, Bill>(@"
                    SELECT
                        Bill.Id,
                        Bill.Sum,
                        Bill.Number,
                        Bill.CreatedAt,
                        Bill.PayedAt,
                        Client.Id,
                        Client.Name,
                        Client.Inn
                    FROM Bill
                    JOIN Client ON Client.Id = Bill.ClientId
                    WHERE Bill.CreatedAt >= @Start AND Bill.CreatedAt <= @End
                    ORDER BY CreatedAt DESC
                    LIMIT 1", (bill, client) =>
                {
                    bill.GetType().GetProperty("Client").SetValue(bill, client);

                    return bill;
                }, new
                {
                    Start = criterion.StartDateTime,
                    End = criterion.EndDateTime
                })
                .FirstOrDefault();
        }
    }
}
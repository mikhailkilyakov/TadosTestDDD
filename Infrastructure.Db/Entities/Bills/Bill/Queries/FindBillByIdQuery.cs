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

    public class FindBillByIdQuery : IQuery<FindById, Bill>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindBillByIdQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public Bill Ask(FindById criterion)
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
                    WHERE Bill.Id = @Id", (bill, client) =>
                {
                    bill.GetType().GetProperty("Client").SetValue(bill, client);

                    return bill;
                }, new
                {
                    Id = criterion.Id
                })
                .FirstOrDefault();
        }
    }
}
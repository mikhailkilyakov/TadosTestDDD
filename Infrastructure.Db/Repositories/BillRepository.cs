namespace Infrastructure.Db.Repositories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Dapper;
    using Domain.Entities.Bills;
    using Domain.Entities.Clients;
    using Domain.Repositories;
    using Transactions;

    public class BillRepository : IRepository<Bill>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public BillRepository(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public Bill Get(int id)
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
                    Id = id
                })
                .FirstOrDefault();
        }

        public void Add(Bill entity)
        {
            if (entity.Id != 0)
                throw new InvalidOperationException("Entity already has id");

            _dbTransactionProvider.CurrentTransaction.Connection.Execute(@"
                INSERT INTO Bill
                (
                    ClientId,
                    Sum,
                    Number,
                    CreatedAt,
                    PayedAt
                )
                VALUES 
                (
                    @ClientId,
                    @Sum,
                    @Number,
                    @CreatedAt,
                    @PayedAt
                );", new
            {
                ClientId = entity.Client.Id,
                Sum = entity.Sum,
                Number = entity.Number,
                CreatedAt = entity.CreatedAt,
                PayedAt = entity.PayedAt
            });

            entity.Id = (int)_dbTransactionProvider.CurrentTransaction.Connection.QueryFirst<long>("SELECT last_insert_rowid();");
        }

        public void Save(Bill entity)
        {
            if (entity.Id == 0)
                throw new InvalidOperationException("Entity doesn't have id");

            _dbTransactionProvider.CurrentTransaction.Connection.Execute(@"
                UPDATE Bill
                SET
                    ClientId = @ClientId,
                    Sum = @Sum,
                    Number = @Number,
                    CreatedAt = @CreatedAt,
                    PayedAt = @PayedAt
                WHERE Id = @Id;", new
            {
                Id = entity.Id,
                ClientId = entity.Client.Id,
                Sum = entity.Sum,
                Number = entity.Number,
                CreatedAt = entity.CreatedAt,
                PayedAt = entity.PayedAt
            });
        }

        public void Delete(Bill entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
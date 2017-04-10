namespace Infrastructure.Db.Repositories
{
    using System;
    using Dapper;
    using Domain.Commands;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Domain.Repositories;
    using Transactions;

    public class ClientRepository : IRepository<Client>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public ClientRepository(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public Client Get(int id)
        {
            return _dbTransactionProvider.CurrentTransaction.Connection.QueryFirstOrDefault<Client>(@"
                SELECT 
                    Id, 
                    Name, 
                    Inn
                FROM Client
                WHERE Id = @Id; ", new { Id = id });
        }

        public void Add(Client entity)
        {
            if (entity.Id != 0)
                throw new InvalidOperationException("Entity already has id");

            _dbTransactionProvider.CurrentTransaction.Connection.Execute("INSERT INTO Client (Name, Inn) VALUES (@Name, @Inn);", new { Name = entity.Name, Inn = entity.Inn });
            entity.Id = (int)_dbTransactionProvider.CurrentTransaction.Connection.QueryFirst<long>("SELECT last_insert_rowid();");
        }

        public void Save(Client entity)
        {
            if (entity.Id == 0)
                throw new InvalidOperationException("Entity doesn't have id");

            _dbTransactionProvider.CurrentTransaction.Connection.Execute("UPDATE Client SET Name = @Name, Inn = @Inn WHERE Id = @Id", new
            {
                Id = entity.Id,
                Name = entity.Name,
                Inn = entity.Inn
            });
        }

        public void Delete(Client entity)
        {
            _dbTransactionProvider.CurrentTransaction.Connection.Execute("DELETE FROM Client WHERE Id = @Id;", new { Id = entity.Id });
        }
    }
}
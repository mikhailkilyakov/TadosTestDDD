namespace Domain.Services.Clients.Client
{
    using System;
    using Entities.Clients;
    using Exceptions;
    using Queries;
    using Queries.Criterion;
    using Repositories;

    public class ClientService : IEntityService<Client>, IClientService
    {
        private readonly IRepository<Client> _repository;
        private readonly IQueryBuilder _queryBuilder;

        public ClientService(IRepository<Client> repository, IQueryBuilder queryBuilder)
        {
            _repository = repository;
            _queryBuilder = queryBuilder;
        }

        public void Add(Client entity)
        {
            Client clientFoundByInn = _queryBuilder.For<Client>().With(new FindByInn()
            {
                Inn = entity.Inn
            });

            if (clientFoundByInn != null)
                throw new InvalidOperationException("Client with this INN already exists");

            _repository.Add(entity);
        }

        public Client Get(int id)
        {
            return _repository.Get(id);
        }

        public void ChangeName(Client client, string name)
        {
            client.SetName(name);
        }

        public void Delete(int id)
        {
            Client client = _repository.Get(id);

            if (client == null)
                throw new EntityNotFoundException("Client with specified Id not found");

            _repository.Delete(client);
        }
    }
}
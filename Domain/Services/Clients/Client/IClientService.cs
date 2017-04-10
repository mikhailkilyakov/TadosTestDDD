namespace Domain.Services.Clients.Client
{
    using Entities.Clients;

    public interface IClientService
    {
        void ChangeName(Client client, string name);
    }
}
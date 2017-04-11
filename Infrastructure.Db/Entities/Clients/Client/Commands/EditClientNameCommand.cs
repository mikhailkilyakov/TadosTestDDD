namespace Infrastructure.Db.Entities.Clients.Client.Commands
{
    using Domain.Commands;
    using Domain.Entities.Clients;
    using Domain.Repositories;
    using Domain.Services.Clients.Client;
    using WebApi.Application.Controllers.Client.Commands.Contexts;

    public class EditClientNameCommand : ICommand<EditClientNameCommandContext>
    {
        private readonly IClientService _clientService;
        private readonly IRepository<Client> _clientRepository;

        public EditClientNameCommand(IClientService clientService, IRepository<Client> clientRepository)
        {
            _clientService = clientService;
            _clientRepository = clientRepository;
        }

        public void Execute(EditClientNameCommandContext commandContext)
        {
            _clientService.ChangeName(commandContext.Client, commandContext.Form.Name);
            _clientRepository.Save(commandContext.Client);
        }
    }
}
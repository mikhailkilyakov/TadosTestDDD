namespace Infrastructure.Db.Entities.Clients.Client.Commands
{
    using Domain.Commands;
    using Domain.Entities.Clients;
    using Domain.Repositories;
    using Domain.Services;
    using Transactions;
    using WebApi.Application.Controllers.Client.Commands.Contexts;
    public class CreateClientCommand : ICommand<CreateClientCommandContext>
    {
        private readonly IEntityService<Client> _entityService;

        public CreateClientCommand(IEntityService<Client> entityService)
        {
            _entityService = entityService;
        }

        public void Execute(CreateClientCommandContext commandContext)
        {
            _entityService.Add(new Client(commandContext.Form.Name, commandContext.Form.Inn));
        }
    }
}
namespace Infrastructure.Db.Entities.Clients.Client.Commands
{
    using Domain.Commands;
    using Domain.Entities.Clients;
    using Domain.Services;
    using WebApi.Application.Controllers.Client.Commands.Contexts;

    public class DeleteClientCommand : ICommand<DeleteClientCommandContext>
    {
        private readonly IEntityService<Client> _entityService;

        public DeleteClientCommand(IEntityService<Client> entityService)
        {
            _entityService = entityService;
        }

        public void Execute(DeleteClientCommandContext commandContext)
        {
            _entityService.Delete(commandContext.Form.Id);
        }
    }
}
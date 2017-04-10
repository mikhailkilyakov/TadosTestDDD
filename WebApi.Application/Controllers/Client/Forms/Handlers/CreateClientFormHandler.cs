namespace WebApi.Application.Controllers.Client.Forms.Handlers
{
    using Commands.Contexts;
    using Domain.Commands;
    using Infrastructure.Forms.Handlers;

    public class CreateClientFormHandler : IApiFormHandler<CreateClientForm>
    {
        private readonly ICommandBuilder _commandBuilder;

        public CreateClientFormHandler(ICommandBuilder commandBuilder)
        {
            _commandBuilder = commandBuilder;
        }

        public void Execute(CreateClientForm form)
        {
            _commandBuilder.Execute(new CreateClientCommandContext()
            {
                Form = form
            });
        }
    }
}
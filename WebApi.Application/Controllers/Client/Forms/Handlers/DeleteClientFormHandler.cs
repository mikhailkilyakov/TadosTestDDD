namespace WebApi.Application.Controllers.Client.Forms.Handlers
{
    using Commands.Contexts;
    using Domain.Commands;
    using Infrastructure.Forms.Handlers;
    public class DeleteClientFormHandler : IApiFormHandler<DeleteClientForm>
    {
        private readonly ICommandBuilder _commandBuilder;

        public DeleteClientFormHandler(ICommandBuilder commandBuilder)
        {
            _commandBuilder = commandBuilder;
        }

        public void Execute(DeleteClientForm form)
        {
            _commandBuilder.Execute(new DeleteClientCommandContext()
            {
                Form = form
            });
        }
    }
}
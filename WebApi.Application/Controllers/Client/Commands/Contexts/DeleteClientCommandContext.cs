namespace WebApi.Application.Controllers.Client.Commands.Contexts
{
    using Domain.Commands.Contexts;
    using Forms;

    public class DeleteClientCommandContext : ICommandContext
    {
        public DeleteClientForm Form { get; set; }
    }
}
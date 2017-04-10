namespace WebApi.Application.Controllers.Client.Commands.Contexts
{
    using Domain.Commands.Contexts;
    using Forms;

    public class CreateClientCommandContext : ICommandContext
    {
        public CreateClientForm Form { get; set; }
    }
}
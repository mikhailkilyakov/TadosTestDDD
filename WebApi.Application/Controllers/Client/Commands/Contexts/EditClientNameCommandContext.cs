namespace WebApi.Application.Controllers.Client.Commands.Contexts
{
    using Domain.Commands.Contexts;
    using Domain.Entities.Clients;
    using Forms;

    public class EditClientNameCommandContext : ICommandContext
    {
        public Client Client { get; set; }

        public EditClientNameForm Form { get; set; }
    }
}
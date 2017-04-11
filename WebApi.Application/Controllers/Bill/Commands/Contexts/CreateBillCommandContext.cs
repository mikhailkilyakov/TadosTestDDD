namespace WebApi.Application.Controllers.Bill.Commands.Contexts
{
    using Domain.Commands.Contexts;
    using Domain.Entities.Clients;
    using Forms;

    public class CreateBillCommandContext : ICommandContext
    {
        public Client Client { get; set; }

        public CreateBillForm Form { get; set; }
    }
}
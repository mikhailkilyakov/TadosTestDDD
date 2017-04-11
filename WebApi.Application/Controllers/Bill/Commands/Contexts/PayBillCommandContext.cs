namespace WebApi.Application.Controllers.Bill.Commands.Contexts
{
    using Domain.Commands.Contexts;
    using Domain.Entities.Bills;
    using Forms;

    public class PayBillCommandContext : ICommandContext
    {
        public Bill Bill { get; set; }

        public PayBillForm Form { get; set; }
    }
}
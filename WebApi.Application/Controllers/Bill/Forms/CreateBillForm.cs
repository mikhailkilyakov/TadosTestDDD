namespace WebApi.Application.Controllers.Bill.Forms
{
    using Infrastructure.Forms;

    public class CreateBillForm : IApiForm
    {
        public int ClientId { get; set; }

        public decimal Sum { get; set; }
    }
}
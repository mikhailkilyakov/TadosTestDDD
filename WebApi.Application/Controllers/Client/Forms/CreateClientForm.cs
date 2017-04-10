namespace WebApi.Application.Controllers.Client.Forms
{
    using Infrastructure.Forms;

    public class CreateClientForm : IApiForm
    {
        public string Name { get; set; }

        public string Inn { get; set; }
    }
}
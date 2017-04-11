namespace WebApi.Application.Controllers.Client.Forms
{
    using Infrastructure.Forms;

    public class DeleteClientForm : IApiForm
    {
        public int Id { get; set; }
    }
}
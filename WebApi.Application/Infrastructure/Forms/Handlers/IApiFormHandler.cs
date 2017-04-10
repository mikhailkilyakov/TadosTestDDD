namespace WebApi.Application.Infrastructure.Forms.Handlers
{
    public interface IApiFormHandler<in TForm> where TForm : IApiForm
    {
        void Execute(TForm form);
    }
}
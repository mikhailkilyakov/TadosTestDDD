namespace WebApi.Application.Infrastructure.Forms
{
    using Handlers;

    public interface IApiFormHandlerFactory
    {
        IApiFormHandler<TForm> Create<TForm>() where TForm : IApiForm;
    }
}
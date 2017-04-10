namespace WebApi.Application.Infrastructure.Controllers
{
    using Forms;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ApiFormControllerBase : Controller
    {
        private readonly IApiFormHandlerFactory _apiFormHandlerFactory;

        protected ApiFormControllerBase(IApiFormHandlerFactory apiFormHandlerFactory)
        {
            _apiFormHandlerFactory = apiFormHandlerFactory;
        }

        protected void Form<TForm>(TForm form) where TForm : IApiForm
        {
            _apiFormHandlerFactory.Create<TForm>().Execute(form);
        }
    }
}
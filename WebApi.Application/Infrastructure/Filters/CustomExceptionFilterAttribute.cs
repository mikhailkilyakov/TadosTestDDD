namespace WebApi.Application.Infrastructure.Filters
{
    using Domain.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            if (context.Exception is EntityNotFoundException)
                context.HttpContext.Response.StatusCode = 404;
            else
                context.HttpContext.Response.StatusCode = 500;

            context.Result = new JsonResult(new
            {
                Error = context.Exception.Message
            });
        }
    }
}
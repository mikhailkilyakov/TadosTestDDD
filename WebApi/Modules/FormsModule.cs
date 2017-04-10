namespace WebApi.Modules
{
    using System.Reflection;
    using Application.Controllers.Client.Forms;
    using Application.Controllers.Client.Forms.Handlers;
    using Application.Infrastructure.Forms;
    using Application.Infrastructure.Forms.Handlers;
    using Autofac;
    using global::Autofac;
    using Module = global::Autofac.Module;

    public class FormsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateClientFormHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IApiFormHandler<>));

            builder.RegisterTypedFactory<IApiFormHandlerFactory>();
        }
    }
}
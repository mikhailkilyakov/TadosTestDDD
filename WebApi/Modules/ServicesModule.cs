namespace WebApi.Modules
{
    using System.Reflection;
    using Domain.Services;
    using Domain.Services.Clients.Client;
    using global::Autofac;
    using Module = global::Autofac.Module;

    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ClientService).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEntityService<>))
                .AsImplementedInterfaces();
        }
    }
}
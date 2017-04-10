namespace WebApi.Modules
{
    using System.Reflection;
    using Autofac;
    using Domain.Commands;
    using global::Autofac;
    using Infrastructure.Db.Entities.Clients.Client.Commands;
    using Module = global::Autofac.Module;

    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateClientCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(ICommand<>));
            builder.RegisterType<CommandBuilder>().As<ICommandBuilder>();
            builder.RegisterTypedFactory<ICommandFactory>();
        }
    }
}
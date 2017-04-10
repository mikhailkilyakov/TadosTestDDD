namespace WebApi.Modules
{
    using System.Reflection;
    using Domain.Repositories;
    using global::Autofac;
    using Infrastructure.Db.Commands;
    using Infrastructure.Db.Connections;
    using Infrastructure.Db.Repositories;
    using Infrastructure.Db.Sqlite;
    using Microsoft.Extensions.Configuration;
    using Module = global::Autofac.Module;

    public class SqliteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ClientRepository).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRepository<>));
            builder.RegisterType<SqliteCommandFactory>().As<IDbCommandFactory>();
            builder.Register(c =>
                    new SqliteConnectionFactory(
                        c.Resolve<IConfigurationRoot>().GetConnectionString("TestConnectionString")))
                .As<IDbConnectionFactory>().SingleInstance();
        }
    }
}
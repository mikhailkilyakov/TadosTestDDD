namespace WebApi.Modules
{
    using System.Reflection;
    using Autofac;
    using Domain.Queries;
    using global::Autofac;
    using Infrastructure.Db.Entities.Clients.Client.Commands;
    using Infrastructure.Db.Entities.Clients.Client.Queries;
    using Module = global::Autofac.Module;

    public class QueriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(FindClientByIdQuery).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IQuery<,>));
            builder.RegisterGeneric(typeof(QueryFor<>)).As(typeof(IQueryFor<>));
            builder.RegisterTypedFactory<IQueryBuilder>();
            builder.RegisterTypedFactory<IQueryFactory>();
        }
    }
}
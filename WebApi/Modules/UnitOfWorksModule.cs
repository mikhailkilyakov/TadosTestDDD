namespace WebApi.Modules
{
    using Domain.UnitOfWork;
    using global::Autofac;
    using Infrastructure.Db.UnitOfWork;

    public class UnitOfWorksModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DbUnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope(); //per request for web api
            builder.RegisterType<DbUnitOfWork>().As<IUnitOfWork>();
        }
    }
}
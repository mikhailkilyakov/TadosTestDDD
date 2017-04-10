namespace WebApi.Modules
{
    using global::Autofac;
    using Infrastructure.Db.Transactions;

    public class TransactionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DbTransactionProvider>().As<IDbTransactionProvider>().InstancePerLifetimeScope(); //per request for web api
        }
    }
}
namespace Domain.Queries
{
    public interface IQueryBuilder
    {
        IQueryFor<TResult> For<TResult>();
    }
}
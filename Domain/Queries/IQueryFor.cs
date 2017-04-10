namespace Domain.Queries
{
    using Criterion;

    public interface IQueryFor<out TResult>
    {
        TResult With<TCriterion>(TCriterion criterion) where TCriterion : ICriterion;
    }
}
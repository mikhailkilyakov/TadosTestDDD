namespace Domain.Queries
{
    using Criterion;

    public interface IQueryFactory
    {
        IQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion; 
    }
}
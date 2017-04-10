namespace Domain.Queries
{
    using Criterion;

    public interface IQuery<in TCriterion, out TResult> where TCriterion : ICriterion
    {
        TResult Ask(TCriterion criterion);
    }
}
namespace WebApi.Application.Infrastructure.Queries.Criteria
{
    using Domain.Queries.Criterion;

    public class FindByCountAndOffset : ICriterion
    {
        public int Count { get; set; }

        public int Offset { get; set; }
    }
}
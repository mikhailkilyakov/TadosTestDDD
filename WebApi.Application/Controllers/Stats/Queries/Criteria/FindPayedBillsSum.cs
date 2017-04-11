namespace WebApi.Application.Controllers.Stats.Queries.Criteria
{
    using System;
    using Domain.Queries.Criterion;

    public class FindPayedBillsSum : ICriterion
    {
        public int Count { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }
    }
}
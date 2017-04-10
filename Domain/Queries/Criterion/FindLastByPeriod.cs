namespace Domain.Queries.Criterion
{
    using System;

    public class FindLastByPeriod : ICriterion
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
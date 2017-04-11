namespace Infrastructure.Db.Dto.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    using Domain.Dto;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Transactions;
    using WebApi.Application.Controllers.Stats.Queries.Criteria;

    public class FindPayedBillsSumQuery : IQuery<FindPayedBillsSum, IEnumerable<PayedBillsSumDto>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindPayedBillsSumQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public IEnumerable<PayedBillsSumDto> Ask(FindPayedBillsSum criterion)
        {
            string additionalWhereConditionsString = string.Empty;

            List<string> additionalWhereConditions = new List<string>();

            if (criterion.StartDateTime.HasValue)
                additionalWhereConditions.Add("Bill.PayedAt >= @StartDateTime");

            if (criterion.EndDateTime.HasValue)
                additionalWhereConditions.Add("Bill.PayedAt <= @EndDateTime");

            if (additionalWhereConditions.Count > 0)
                additionalWhereConditionsString = $"AND {string.Join(" AND ", additionalWhereConditions)}";

            return _dbTransactionProvider.CurrentTransaction.Connection
                .Query<PayedBillsSumDto, Client, PayedBillsSumDto>($@"
                    SELECT
                    	CAST(t.`Sum` AS REAL) `Sum`,
                    	Client.Id,
                    	Client.Name,
                    	Client.Inn
                    FROM Client
                    JOIN
                    	(SELECT Client.Id, SUM(Bill.`Sum`) `Sum`
                    	FROM Bill
                    	JOIN Client ON Bill.ClientId = Client.Id
                    	WHERE Bill.PayedAt IS NOT NULL {additionalWhereConditionsString}
                    	GROUP BY Client.Id) t ON Client.Id = t.Id
                    ORDER BY t.`Sum` DESC
                    LIMIT @Limit;", (dto, client) =>
                {
                    dto.Client = client;

                    return dto;
                }, new
                {
                    Limit = criterion.Count,
                    StartDateTime = criterion.StartDateTime,
                    EndDateTime = criterion.EndDateTime
                });
        }
    }
}
namespace Infrastructure.Db.Dto.Queries
{
    using System.Collections.Generic;
    using Dapper;
    using Domain.Dto;
    using Domain.Queries;
    using Transactions;
    using WebApi.Application.Controllers.Stats.Queries.Criteria;
    public class FindClientBillsStatsQuery : IQuery<FindClientBillsStats, BillsStatsDto>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindClientBillsStatsQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public BillsStatsDto Ask(FindClientBillsStats criterion)
        {
            string additionalWhereConditionsString = string.Empty;

            List<string> additionalWhereConditions = new List<string>();

            if (criterion.StartDateTime.HasValue)
                additionalWhereConditions.Add("Bill.CreatedAt >= @StartDateTime");

            if (criterion.EndDateTime.HasValue)
                additionalWhereConditions.Add("Bill.CreatedAt <= @EndDateTime");

            if (additionalWhereConditions.Count > 0)
                additionalWhereConditionsString = $"AND {string.Join(" AND ", additionalWhereConditions)}";

            return _dbTransactionProvider.CurrentTransaction.Connection.QueryFirstOrDefault<BillsStatsDto>($@"
                SELECT
                	CAST(COALESCE(payed.`Sum`, 0) AS REAL) `PayedSum`,
                	COALESCE(payed.`Count`, 0) `PayedCount`,
                	CAST(COALESCE(unpayed.`Sum`, 0) AS REAL) `UnpayedSum`,
                	COALESCE(unpayed.`Count`, 0) `UnpayedCount`	
                FROM Client
                LEFT JOIN
                	(SELECT Bill.ClientId, SUM(Bill.Sum) `Sum`, COUNT(1) `Count`
                	FROM Bill
                	WHERE Bill.PayedAt IS NULL {additionalWhereConditionsString}
                	GROUP BY Bill.ClientId) unpayed ON unpayed.ClientId = Client.Id
                LEFT JOIN
                	(SELECT Bill.ClientId, SUM(Bill.Sum) `Sum`, COUNT(1) `Count`
                	FROM Bill
                	WHERE Bill.PayedAt IS NOT NULL {additionalWhereConditionsString}
                	GROUP BY Bill.ClientId) payed ON payed.ClientId = Client.Id
                WHERE Client.Id = @ClientId;", new
            {
                ClientId = criterion.ClientId,
                StartDateTime = criterion.StartDateTime,
                EndDateTime = criterion.EndDateTime
            });
        }
    }
}
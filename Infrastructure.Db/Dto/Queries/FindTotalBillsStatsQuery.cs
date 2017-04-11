namespace Infrastructure.Db.Dto.Queries
{
    using Dapper;
    using Domain.Dto;
    using Domain.Queries;
    using Transactions;
    using WebApi.Application.Controllers.Stats.Queries.Criteria;

    public class FindTotalBillsStatsQuery : IQuery<FindTotalBillsStats, BillsStatsDto>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;

        public FindTotalBillsStatsQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider = dbTransactionProvider;
        }

        public BillsStatsDto Ask(FindTotalBillsStats criterion)
        {
            return _dbTransactionProvider.CurrentTransaction.Connection
                .QueryFirstOrDefault<BillsStatsDto>(@"
                    SELECT
                    	CAST(COALESCE(payed.`Sum`, 0) AS REAL) `PayedSum`,
                    	COALESCE(payed.`Count`, 0) `PayedCount`,
                    	CAST(COALESCE(unpayed.`Sum`, 0) AS REAL) `UnpayedSum`,
                    	COALESCE(unpayed.`Count`, 0) `UnpayedCount`	
                    FROM
                    	(SELECT 
                    		SUM(`Sum`) `Sum`, 
                    		COUNT(1) `Count`
                    	FROM Bill WHERE PayedAt IS NULL) unpayed
                    JOIN (SELECT 
                    		SUM(`Sum`) `Sum`, 
                    		COUNT(1) `Count`
                    	FROM Bill WHERE PayedAt IS NOT NULL) payed ON 1 = 1;");
        }
    }
}
namespace Domain.Dto
{
    public class BillsStatsDto
    {
        public int TotalCount => PayedCount + UnpayedCount;

        public int PayedCount { get; set; }

        public int UnpayedCount { get; set; }

        public decimal TotalSum => PayedSum + UnpayedSum;

        public decimal PayedSum { get; set; }

        public decimal UnpayedSum { get; set; }
    }
}
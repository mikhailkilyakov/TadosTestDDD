namespace WebApi.Application.Infrastructure.Queries.Criteria
{
    public class FindByCountAndOffsetAndClientId : FindByCountAndOffset
    {
        public int ClientId { get; set; }
    }
}
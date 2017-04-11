namespace Domain.Dto
{
    using Entities.Clients;

    public class PayedBillsSumDto
    {
        public Client Client { get; set; }

        public decimal Sum { get; set; }
    }
}
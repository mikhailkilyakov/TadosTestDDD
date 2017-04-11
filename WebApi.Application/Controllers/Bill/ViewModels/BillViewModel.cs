namespace WebApi.Application.Controllers.Bill.ViewModels
{
    public class BillViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string CreatedAt { get; set; }

        public string PayedAt { get; set; }

        public decimal Sum { get; set; }

        public string DisplayNumber { get; set; }

        public bool WasPayed { get; set; }
    }
}
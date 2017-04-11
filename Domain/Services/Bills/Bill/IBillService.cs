namespace Domain.Services.Bills.Bill
{
    using Entities.Bills;

    public interface IBillService
    {
        void Pay(Bill bill);
    }
}
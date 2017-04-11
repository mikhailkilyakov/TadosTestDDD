namespace Infrastructure.Db.Entities.Bills.Bill.Commands
{
    using Domain.Commands;
    using Domain.Entities.Bills;
    using Domain.Exceptions;
    using Domain.Repositories;
    using WebApi.Application.Controllers.Bill.Commands.Contexts;

    public class PayBillCommand : ICommand<PayBillCommandContext>
    {
        private readonly IRepository<Bill> _repository;

        public PayBillCommand(IRepository<Bill> repository)
        {
            _repository = repository;
        }

        public void Execute(PayBillCommandContext commandContext)
        {
            if (commandContext.Bill == null)
                throw new EntityNotFoundException("Bill not found");

            commandContext.Bill.Pay();

            _repository.Save(commandContext.Bill);
        }
    }
}
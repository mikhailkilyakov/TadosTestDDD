namespace Infrastructure.Db.Entities.Bills.Bill.Commands
{
    using Domain.Commands;
    using Domain.Entities.Bills;
    using Domain.Services;
    using WebApi.Application.Controllers.Bill.Commands.Contexts;

    public class CreateBillCommand : ICommand<CreateBillCommandContext>
    {
        private readonly IEntityService<Bill> _entityService;

        public CreateBillCommand(IEntityService<Bill> entityService)
        {
            _entityService = entityService;
        }

        public void Execute(CreateBillCommandContext commandContext)
        {
            _entityService.Add(new Bill(commandContext.Client, commandContext.Form.Sum));
        }
    }
}
namespace WebApi.Application.Controllers.Bill.Forms.Handlers
{
    using Commands.Contexts;
    using Domain.Commands;
    using Domain.Entities.Bills;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Infrastructure.Forms.Handlers;

    public class PayBillFormHandler : IApiFormHandler<PayBillForm>
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public PayBillFormHandler(IQueryBuilder queryBuilder, ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }

        public void Execute(PayBillForm form)
        {
            Bill bill = _queryBuilder.For<Bill>().With(new FindById()
            {
                Id = form.Id
            });

            _commandBuilder.Execute(new PayBillCommandContext()
            {
                Bill = bill,
                Form = form
            });
        }
    }
}
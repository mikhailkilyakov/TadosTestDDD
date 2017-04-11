namespace WebApi.Application.Controllers.Bill.Forms.Handlers
{
    using Commands.Contexts;
    using Domain.Commands;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Infrastructure.Forms.Handlers;

    public class CreateBillFormHandler : IApiFormHandler<CreateBillForm>
    {
        private readonly ICommandBuilder _commandBuilder;
        private readonly IQueryBuilder _queryBuilder;

        public CreateBillFormHandler(ICommandBuilder commandBuilder, IQueryBuilder queryBuilder)
        {
            _commandBuilder = commandBuilder;
            _queryBuilder = queryBuilder;
        }

        public void Execute(CreateBillForm form)
        {
            Client client = _queryBuilder.For<Client>().With(new FindById()
            {
                Id = form.ClientId
            });
            
            _commandBuilder.Execute(new CreateBillCommandContext()
            {
                Client = client,
                Form = form
            });
        }
    }
}
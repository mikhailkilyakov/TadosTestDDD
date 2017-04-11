namespace WebApi.Application.Controllers.Client.Forms.Handlers
{
    using Commands.Contexts;
    using Domain.Commands;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Infrastructure.Forms.Handlers;
    public class EdtiClientNameFormHandler : IApiFormHandler<EditClientNameForm>
    {
        private readonly ICommandBuilder _commandBuilder;
        private readonly IQueryBuilder _queryBuilder;

        public EdtiClientNameFormHandler(ICommandBuilder commandBuilder, IQueryBuilder queryBuilder)
        {
            _commandBuilder = commandBuilder;
            _queryBuilder = queryBuilder;
        }

        public void Execute(EditClientNameForm form)
        {
            Client client = _queryBuilder.For<Client>().With(new FindById()
            {
                Id = form.Id
            });

            _commandBuilder.Execute(new EditClientNameCommandContext()
            {
                Client = client,
                Form = form
            });
        }
    }
}
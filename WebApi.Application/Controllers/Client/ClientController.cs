namespace WebApi.Application.Controllers.Client
{
    using System.Collections.Generic;
    using AutoMapper;
    using Bill.ViewModels;
    using Domain.Entities.Bills;
    using Domain.Entities.Clients;
    using Domain.Exceptions;
    using Domain.Queries;
    using Domain.Queries.Criterion;
    using Domain.UnitOfWork;
    using Forms;
    using Infrastructure.Controllers;
    using Infrastructure.Forms;
    using Infrastructure.Queries.Criteria;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    public class ClientController : ApiFormControllerBase
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public ClientController(
            IApiFormHandlerFactory apiFormHandlerFactory,
            IUnitOfWorkFactory unitOfWorkFactory,
            IQueryBuilder queryBuilder,
            IMapper mapper) : base(apiFormHandlerFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _queryBuilder = queryBuilder;
            _mapper = mapper;
        }

        [HttpPost]
        public void Add([FromBody] CreateClientForm form)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Form(form);

                unitOfWork.Commit();
            }
        }

        [HttpGet]
        public IEnumerable<ClientViewModel> List(int offset, int count)
        {
            if (offset < 0)
                offset = 0;

            if (count < 0)
                count = 0;

            if (count > 100)
                count = 100;

            IEnumerable<Client> clients;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                clients = _queryBuilder.For<IEnumerable<Client>>()
                    .With(new FindByCountAndOffset()
                    {
                        Count = count,
                        Offset = offset
                    });

                unitOfWork.Commit();
            }

            return _mapper.Map<IEnumerable<ClientViewModel>>(clients);
        }

        [HttpPut]
        public void ChangeName(int id, [FromBody] EditClientNameForm form)
        {
            form.Id = id;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Form(form);

                unitOfWork.Commit();
            }
        }

        [HttpDelete("[controller]/{id}")]
        public void Delete(int id)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Form(new DeleteClientForm()
                {
                    Id = id
                });

                unitOfWork.Commit();
            }
        }

        [HttpGet("[controller]/{id}")]
        public ClientViewModel Get(int id)
        {
            Client client;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                client = _queryBuilder.For<Client>().With(new FindById()
                {
                    Id = id
                });

                unitOfWork.Commit();
            }

            if (client == null)
                throw new EntityNotFoundException("Client not found");

            return _mapper.Map<ClientViewModel>(client);
        }

        [HttpGet]
        public IEnumerable<BillViewModel> Bills(int id, int offset, int count)
        {
            IEnumerable<Bill> bills;

            if (offset < 0)
                offset = 0;

            if (count < 0)
                count = 0;

            if (count > 100)
                count = 100;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                bills = _queryBuilder.For<IEnumerable<Bill>>().With(new FindByCountAndOffsetAndClientId()
                {
                    ClientId = id,
                    Count = count,
                    Offset = offset
                });

                unitOfWork.Commit();
            }

            return _mapper.Map<IEnumerable<BillViewModel>>(bills);
        }
    }
}
namespace WebApi.Application.Controllers.Client
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.Clients;
    using Domain.Queries;
    using Domain.UnitOfWork;
    using Forms;
    using Infrastructure.Controllers;
    using Infrastructure.Forms;
    using Infrastructure.Queries.Criteria;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    public class ClientController :  ApiFormControllerBase
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
    }
}
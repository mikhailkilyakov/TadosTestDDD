namespace WebApi.Application.Controllers.Bill
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.Bills;
    using Domain.Queries;
    using Domain.UnitOfWork;
    using Forms;
    using Infrastructure.Controllers;
    using Infrastructure.Forms;
    using Infrastructure.Queries.Criteria;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    public class BillController : ApiFormControllerBase
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public BillController(
            IApiFormHandlerFactory apiFormHandlerFactory,
            IUnitOfWorkFactory unitOfWorkFactory,
            IQueryBuilder queryBuilder,
            IMapper mapper) : base(apiFormHandlerFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _queryBuilder = queryBuilder;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<BillViewModel> List(int offset, int count)
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
                bills = _queryBuilder.For<IEnumerable<Bill>>().With(new FindByCountAndOffset()
                {
                    Count = count,
                    Offset = offset
                });

                unitOfWork.Commit();
            }

            return _mapper.Map<IEnumerable<BillViewModel>>(bills);
        }

        [HttpPost]
        public void Add([FromBody] CreateBillForm form)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Form(form);

                unitOfWork.Commit();
            }
        }

        [HttpPut]
        public void Pay(int id)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Form(new PayBillForm()
                {
                    Id = id
                });

                unitOfWork.Commit();
            }
        }
    }
}
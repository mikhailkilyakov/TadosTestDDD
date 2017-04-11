namespace WebApi.Application.Controllers.Stats
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Domain;
    using Domain.Dto;
    using Domain.Exceptions;
    using Domain.Queries;
    using Domain.UnitOfWork;
    using Infrastructure.Controllers;
    using Infrastructure.Forms;
    using Microsoft.AspNetCore.Mvc;
    using Queries.Criteria;

    public class StatsController : ApiFormControllerBase
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IQueryBuilder _queryBuilder;


        public StatsController(
            IApiFormHandlerFactory apiFormHandlerFactory,
            IUnitOfWorkFactory unitOfWorkFactory,
            IQueryBuilder queryBuilder)
            : base(apiFormHandlerFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public IEnumerable<PayedBillsSumDto> PayedBillsSum(int count, string startDateTime, string endDateTime)
        {
            DateTime? start = null;
            DateTime? end = null;

            if (!string.IsNullOrWhiteSpace(startDateTime))
            {
                start = DateTime.ParseExact(startDateTime, DateTimeFormatProvider.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(endDateTime))
            {
                end = DateTime.ParseExact(startDateTime, DateTimeFormatProvider.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            if (count < 0)
                count = 0;

            if (count > 100)
                count = 100;

            IEnumerable<PayedBillsSumDto> result;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                result = _queryBuilder.For<IEnumerable<PayedBillsSumDto>>().With(new FindPayedBillsSum()
                {
                    Count = count,
                    StartDateTime = start,
                    EndDateTime = end
                });

                unitOfWork.Commit();
            }

            return result;
        }

        [HttpGet("[controller]/Client/{id}/Bills")]
        public BillsStatsDto ClientBills(int id, string startDateTime, string endDateTime)
        {
            DateTime? start = null;
            DateTime? end = null;

            if (!string.IsNullOrWhiteSpace(startDateTime))
            {
                start = DateTime.ParseExact(startDateTime, DateTimeFormatProvider.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(endDateTime))
            {
                end = DateTime.ParseExact(startDateTime, DateTimeFormatProvider.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            BillsStatsDto result = null;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                result = _queryBuilder.For<BillsStatsDto>().With(new FindClientBillsStats()
                {
                    ClientId = id,
                    StartDateTime = start,
                    EndDateTime = end
                });

                unitOfWork.Commit();
            }

            if (result == null)
                throw new EntityNotFoundException("Client not found");

            return result;
        }

        [HttpGet]
        public BillsStatsDto Bills()
        {
            BillsStatsDto result = null;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                result = _queryBuilder.For<BillsStatsDto>().With(new FindTotalBillsStats());

                unitOfWork.Commit();
            }

            return result;
        }
    }
}
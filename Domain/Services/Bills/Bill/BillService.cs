namespace Domain.Services.Bills.Bill
{
    using System;
    using Entities.Bills;
    using Queries;
    using Queries.Criterion;
    using Repositories;

    public class BillService : IEntityService<Bill>
    {
        private readonly IRepository<Bill> _repository;
        private readonly IQueryBuilder _queryBuilder;

        public BillService(IRepository<Bill> repository, IQueryBuilder queryBuilder)
        {
            _repository = repository;
            _queryBuilder = queryBuilder;
        }

        public void Add(Bill entity)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // костыли-костылики

            Bill lastBillInMonth = _queryBuilder.For<Bill>().With(new FindLastByPeriod()
            {
                StartDateTime = firstDayOfMonth,
                EndDateTime = lastDayOfMonth 
            });
            
            int number = lastBillInMonth?.Number ?? 1;

            entity.SetNumber(number);

            _repository.Add(entity);
        }

        public Bill Get(int id)
        {
            return _repository.Get(id);
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
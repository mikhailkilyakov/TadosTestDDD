namespace WebApi.Application.Controllers.Bill.Profiles
{
    using AutoMapper;
    using Domain;
    using Domain.Entities.Bills;
    using ViewModels;

    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<Bill, BillViewModel>()
                .ForMember(x => x.CreatedAt, x => x.MapFrom(y => y.CreatedAt.ToString(DateTimeFormatProvider.DateTimeFormat)))
                .ForMember(x => x.PayedAt, x => x.MapFrom(y => y.PayedAt.HasValue ? y.PayedAt.Value.ToString(DateTimeFormatProvider.DateTimeFormat) : string.Empty));
        }
    }
}
namespace WebApi.Application.Controllers.Client.Profiles
{
    using AutoMapper;
    using Domain.Entities.Clients;
    using ViewModels;

    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientViewModel>();
        }
    }
}
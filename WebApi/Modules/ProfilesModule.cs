namespace WebApi.Modules
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Application.Controllers.Client.Profiles;
    using AutoMapper;
    using global::Autofac;
    using Module = global::Autofac.Module;

    public class ProfilesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ClientProfile).GetTypeInfo().Assembly).As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg => c.Resolve<IEnumerable<Profile>>().ToList().ForEach(cfg.AddProfile)));
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }
    }
}
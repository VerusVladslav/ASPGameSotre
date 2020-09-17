using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStoreCoursework.Utils
{
    public static class AutofacConfiguration
    {
        public static void ConfigurateAutofac()
        {
            //1
            var builder = new ContainerBuilder();

            //2
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //3


            builder.RegisterType<ApplicationContext>().As<DbContext>().SingleInstance();
            builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<GameService>().As<IGameService>();



            //4

            var mapperconfig = new MapperConfiguration(x => x.AddProfile(new MapperConfig()));

            builder.RegisterInstance<IMapper>(mapperconfig.CreateMapper());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}
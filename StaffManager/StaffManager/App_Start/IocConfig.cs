using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using StaffManager.IServices.StaffManage;
using StaffManager.Services.StaffManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace StaffManager.App_Start
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<StaffService>().As<IStaffService>();

            //autofac 注册依赖
            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
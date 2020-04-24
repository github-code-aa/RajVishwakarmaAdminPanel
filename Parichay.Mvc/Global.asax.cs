using Autofac;
using Autofac.Integration.Mvc;
using Nesh.Mvc.Modules;
using Parichay.Mvc.App_Start;
using Parichay.Mvc.Modules;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace Parichay.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //Remeber to Implement
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
        protected void Application_BeginRequest()
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MMM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;
            switch (Request.Url.Scheme)
            {
                case "https":
                    Response.AddHeader("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
                    break;
                case "http":
                    break;
            }
        }
        protected void Session_End(Object sender, EventArgs E)
        {
            Session.Abandon();
            Session.Clear();
        }
    }
}

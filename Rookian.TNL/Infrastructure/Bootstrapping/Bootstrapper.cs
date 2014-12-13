using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Rookian.TNL.App_Start;
using Rookian.TNL.Infrastructure.MVC;
using SimpleInjector;

namespace Rookian.TNL.Infrastructure.Bootstrapping
{
    public class Bootstrapper
    {
        public Bootstrapper BootMVC()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
            
            return this;
        }

        public Container BootDIContainer()
        {
            var container = new Container();

            container.RegisterFubuMvcTagHelpers();
            container.RegisterMvcIntegratedFilterProvider();
            container.RegisterMediator();
            container.ConfigureDependencyResolver();

            return container;
        }
    }
}
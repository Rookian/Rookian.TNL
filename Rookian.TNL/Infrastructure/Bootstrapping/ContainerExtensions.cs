using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Rookian.TNL.Infrastructure.Bootstrapping
{
    public static class ContainerExtensions
    {
        public static Container ConfigureDependencyResolver(this Container container)
        {
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            return container;
        }
    }
}
using System.Web;
using System.Web.Mvc;
using FubuCore;
using FubuCore.Binding;
using FubuCore.Binding.InMemory;
using FubuCore.Binding.Values;
using FubuCore.Logging;
using FubuMVC.Core.Http.AspNet;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI;
using FubuMVC.Core.UI.Elements;
using FubuMVC.Core.UI.Security;
using HtmlTags.Conventions;
using Rookian.TNL.Infrastructure.FubuMVCTagHelper;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web.Mvc;

namespace Rookian.TNL.Infrastructure.Bootstrapping
{
    public static class ContainerExtensions
    {
        public static void ConfigureDependencyResolver(this Container container)
        {
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        public static void RegisterFubuMvcTagHelpers(this Container container)
        {
            var htmlConventionLibrary = new HtmlConventionLibrary();
            htmlConventionLibrary.Import(new DefaultHtmlConventions().Library);
            var conventions = new OverrideHtmlConventions();
            htmlConventionLibrary.Import(conventions.Library);
            container.RegisterSingle(() => htmlConventionLibrary);

            container.RegisterAll<IValueSource>(typeof(RequestPropertyValueSource));
            container.RegisterAll<ITagRequestActivator>(typeof(ElementRequestActivator));

            container.RegisterPerWebRequest<HttpRequestBase>(container.GetInstance<HttpRequestWrapper>);
            container.RegisterPerWebRequest<HttpContextBase>(container.GetInstance<HttpContextWrapper>);
            container.RegisterPerWebRequest(() => HttpContext.Current.Request);
            container.RegisterPerWebRequest(() => HttpContext.Current);
            container.Register<ITypeResolverStrategy, TypeResolver.DefaultStrategy>();
            container.Register<IElementNamingConvention, DefaultElementNamingConvention>();
            container.RegisterOpenGeneric(typeof(ITagGenerator<>), typeof(TagGenerator<>));
            container.RegisterOpenGeneric(typeof(IElementGenerator<>), typeof(ElementGenerator<>));

            container.Register<IFubuRequest, FubuRequest>();
            container.Register<ITypeResolver, TypeResolver>();
            container.Register<ITagGeneratorFactory, TagGeneratorFactory>();
            container.Register<IFieldAccessService, FieldAccessService>();
            container.Register<ITagRequestBuilder, TagRequestBuilder>();
            container.Register<IObjectResolver, ObjectResolver>();
            container.Register<IServiceLocator, InMemoryServiceLocator>();
            container.Register<IBindingLogger, RecordingBindingLogger>();
            container.Register<IBindingHistory, InMemoryBindingHistory>();
            container.Register<ILogger, Logger>();
            container.Register(() => new BindingRegistry());
            container.Register<IRequestData>(() => new RequestData());
        }
    }
}
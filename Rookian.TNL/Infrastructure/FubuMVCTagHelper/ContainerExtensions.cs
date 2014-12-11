using System.Web;
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
using SimpleInjector;
using SimpleInjector.Extensions;

namespace Rookian.TNL.Infrastructure.FubuMVCTagHelper
{
    public static class ContainerExtensions
    {
        public static void RegisterFubuMvcTagHelpers(this Container container)
        {
            var htmlConventionLibrary = new HtmlConventionLibrary();
            htmlConventionLibrary.Import(new DefaultHtmlConventions().Library);
            var conventions = new OverrideHtmlConventions();
            htmlConventionLibrary.Import(conventions.Library);
            container.RegisterSingle(() => htmlConventionLibrary);

            container.Register<IValueSource, RequestPropertyValueSource>();
            container.Register<ITagRequestActivator, ServiceLocatorTagRequestActivator>();
            container.RegisterPerWebRequest<HttpRequestBase>(container.GetInstance<HttpRequestWrapper>);
            container.RegisterPerWebRequest<HttpContextBase>(container.GetInstance<HttpContextWrapper>);
            container.RegisterPerWebRequest(() => HttpContext.Current.Request);
            container.RegisterPerWebRequest(() => HttpContext.Current);
            container.Register<ITypeResolverStrategy, TypeResolver.DefaultStrategy>();
            container.Register<IElementNamingConvention, DotNotationElementNamingConvention>();
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
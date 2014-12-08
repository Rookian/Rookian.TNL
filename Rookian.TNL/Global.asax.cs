using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
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
using Rookian.TNL.App_Start;
using Rookian.TNL.Infrastructure;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace Rookian.TNL
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());

            var container = new Container();

            var htmlConventionLibrary = new HtmlConventionLibrary();
            htmlConventionLibrary.Import(new DefaultHtmlConventions().Library);
            var conventions = new OverrideHtmlConventions();
            htmlConventionLibrary.Import(conventions.Library);
            container.RegisterSingle<HtmlConventionLibrary>(() => htmlConventionLibrary);

            //container.RegisterSingle<IValueSource>().AddInstances(c =>
            //{
            //    c.Type<RequestPropertyValueSource>();
            //});

            //container.RegisterSingle<ITagRequestActivator>().AddInstances(c =>
            //{
            //    c.Type<ElementRequestActivator>();
            //    c.Type<ServiceLocatorTagRequestActivator>();
            //});
            container.RegisterPerWebRequest<HttpRequestBase>(container.GetInstance<HttpRequestWrapper>);
            container.RegisterPerWebRequest<HttpContextBase>(container.GetInstance<HttpContextWrapper>);
            container.RegisterPerWebRequest<HttpRequest>(() => HttpContext.Current.Request);
            container.RegisterPerWebRequest<HttpContext>(() => HttpContext.Current);
            container.Register<ITypeResolverStrategy, TypeResolver.DefaultStrategy>();
            container.Register<IElementNamingConvention, DotNotationElementNamingConvention>();
            container.RegisterOpenGeneric(typeof(ITagGenerator<>), typeof(TagGenerator<>));
            container.RegisterOpenGeneric(typeof(IElementGenerator<>),typeof(ElementGenerator<>));

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
            container.Register<BindingRegistry>(() => new BindingRegistry());
            container.Register<IRequestData>(() => new RequestData());

       
            container.RegisterMvcIntegratedFilterProvider();

            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
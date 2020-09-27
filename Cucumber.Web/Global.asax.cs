using Autofac;
using Autofac.Integration.Mvc;
using Cucumber.Commands;
using Cucumber.Handlers;
using Cucumber.Validation;
using Cucumber.Web.Infrastructure;
using FluentValidation.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cucumber.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacBootstrap();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void AutofacBootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacFluentValidationModule>();
            builder.RegisterFilterProvider();

            // Don't call this multiple times as it will result to multiple registrations
            builder.AddMediatR(typeof(ICommand).Assembly, typeof(IHandler).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            FluentValidationModelValidatorProvider.Configure(
                provider =>
                {
                    provider.ValidatorFactory =
                        new AutofacValidatorFactory(container);
                });
        }
    }
}

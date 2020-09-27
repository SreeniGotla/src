using Autofac;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using Module = Autofac.Module;

namespace Cucumber.Web.Infrastructure
{
    public class MediatRModule : Module
    {
        private readonly Assembly[] _assemblies;
        public MediatRModule(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var openHandlerTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>)
            };

            foreach (var openHandlerType in openHandlerTypes)
            {
                builder.RegisterAssemblyTypes(_assemblies)
                    .AsClosedTypesOf(openHandlerType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(outerContext =>
            {
                var innerContext = outerContext.Resolve<IComponentContext>();

                return type => innerContext.Resolve(type);
            });
        }
    }
}
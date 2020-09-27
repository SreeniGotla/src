using Autofac;
using FluentValidation;
using System.Linq;


namespace Cucumber.Validation
{
    public class AutofacFluentValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}

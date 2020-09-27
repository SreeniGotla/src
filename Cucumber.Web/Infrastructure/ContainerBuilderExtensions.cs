using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cucumber.Web.Infrastructure
{
    public static class ContainerBuilderExtensions
    {
        public static void AddMediatR(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            AddMediatRInternal(builder, assemblies);
        }

        private static void AddMediatRInternal(ContainerBuilder builder, IEnumerable<Assembly> assemblies)
        {
            var enumerableAssemblies = assemblies as Assembly[] ?? assemblies.ToArray();

            if(enumerableAssemblies == null || !enumerableAssemblies.Any() || enumerableAssemblies.All(x => x == null))
            {
                throw new ArgumentNullException(nameof(assemblies), Properties.Resources.MediatorError);
            }

            builder.RegisterModule(new MediatRModule(enumerableAssemblies));
        }
    }
}
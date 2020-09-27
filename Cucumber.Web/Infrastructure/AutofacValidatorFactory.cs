using System;
using Autofac;
using FluentValidation;

namespace Cucumber.Web.Infrastructure
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext _context;

        public AutofacValidatorFactory(IComponentContext context)
        {
            _context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (_context.TryResolve(validatorType, out var validator))
                return (IValidator)validator;

            return null;
        }
    }
}
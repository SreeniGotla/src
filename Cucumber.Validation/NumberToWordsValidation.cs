using Cucumber.Dto.ViewModels;
using FluentValidation;

namespace Cucumber.Validation
{
    public class NumberToWordsValidation : AbstractValidator<NumberToWordsViewModel>
    {
        public NumberToWordsValidation()
        {
            RuleSet("1", () =>
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
                RuleFor(x => x.Number).SetValidator(new FluentValidation.Validators.ScalePrecisionValidator(2, 10));
            });
          
        }
    }
}

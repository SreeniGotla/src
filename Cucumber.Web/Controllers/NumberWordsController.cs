using Cucumber.Commands.NumberToWords;
using Cucumber.Dto;
using Cucumber.Dto.ViewModels;
using Cucumber.Validation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;

namespace Cucumber.Web.Controllers
{
    public class NumberWordsController : AbstractController
    {
        private readonly IMediator _mediator;
        public NumberWordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: NumberWords
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(NumberToWordsViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var validator = new NumberToWordsValidation();
            var results = validator.Validate(model,ruleSet: "1");

            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
                return View(model);
            
               var words = await _mediator.Send(new NumberToWordsCommand(model.Name, model.Number))
                   .ConfigureAwait(false);

            return RedirectToAction("Result", words);
        }

        public ActionResult Result(NumberToWordsDto wordsDto)
        {
            return View(wordsDto);
        }
    }
}
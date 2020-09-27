using Cucumber.Dto;
using MediatR;

namespace Cucumber.Commands.NumberToWords
{
    public class NumberToWordsCommand : IRequest<NumberToWordsDto>
    {
        public NumberToWordsCommand(string name, decimal input)
        {
            Name = name;
            Input = input;
        }

        public string Name { get; }
        public decimal Input { get; }
    }
}

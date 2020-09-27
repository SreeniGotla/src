using Cucumber.Commands.NumberToWords;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Cucumber.Common;
using System;
using Cucumber.Dto;
using System.Text;

namespace Cucumber.Handlers.NumberToWords
{
    public class NumberToWordsHandler : IRequestHandler<NumberToWordsCommand, NumberToWordsDto>
    {
        public Task<NumberToWordsDto> Handle(NumberToWordsCommand request, CancellationToken cancellationToken)
        {
            StringBuilder words = new StringBuilder();

            string[] splitter = request.Input.ToString().Split('.');

            var intPart = Convert.ToInt32(splitter[0]);

            var decPart = splitter.Length > 1 ? Convert.ToInt32(splitter[1]) : 0;

            words.Append(NumberToWords(intPart));

            words.Append(" dollars");

            if (decPart > 0)
            {
                if (words.Length > 0)
                    words.Append(" and ");

                words.Append(NumberToWords(decPart) + " cents");
            }
            var result = new NumberToWordsDto() { Name = request.Name, Words = words.ToString().ToUpper() };

            return Task.FromResult(result);
        }

        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";
              
                if (number < 20)
                    words += Constants.ONESMAP[number];
                else
                {
                    words += Constants.TENSMAP[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + Constants.ONESMAP[number % 10];
                }
            }

            return words;
        }
    }
}

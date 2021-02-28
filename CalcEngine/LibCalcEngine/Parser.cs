using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibCalcEngine
{
    public class Parser: IParser
    {
        public ArrayList Parse(string parseString)
        {
            ArrayList returnTokenList = new ArrayList();

            parseString = PreParse(parseString);

            // Need to either split on spaces or cope with doubled-up negatives eg -1--2
            foreach (string tokenLoop in parseString.Split(' '))
            {
                if (tokenLoop.Length > 0)
                {
                    double parsedDouble;

                    if (double.TryParse(tokenLoop, out parsedDouble))
                    {
                        returnTokenList.Add(parsedDouble);
                    }
                    else
                    {
                        returnTokenList.Add(new CalcOperator(tokenLoop));
                    }
                }
            }

            return returnTokenList;

        }

        private String PreParse(string parseString)
        {
            // Found this regex https://stackoverflow.com/questions/36197468/parsing-a-mathematical-string-expression-in-python-using-regular-expressions
            // Added x and X to the match expression 
            String returnString = Regex.Replace(parseString, @"((-?(?:\d+(?:\.\d+)?))|([-+\/*()xX])|(-?\.\d+))", @"$1 ");

            return returnString;
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibCalcEngine
{
    public interface IParser
    {
        ArrayList Parse(string parseString);
    }

    public class Parser: IParser
    {
        public ArrayList Parse(string parseString)
        {
            ArrayList returnTokenList = new ArrayList();

            // Need to either split on spaces or cope with doubled-up negatives eg -1--2
            foreach (string tokenLoop in parseString.Split(' '))
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


            return returnTokenList;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LibCalcEngine
{
    public class CalculatorAPI : ICalculator
    {
        String expression = "";
        CalcOperator lastOperator = null;

        public void AddNumber(double numberIn)
        {
            expression += numberIn.ToString() + " ";
        }

        public void AddOperator(CalcOperator operatorIn)
        {
            if (lastOperator != null)
            {
                if (lastOperator.Precedence < operatorIn.Precedence)
                {
                    // "it was discovered that A Cool Calculation Company SaaS Product doesn’t have the facility to order the operations (BEDMAS) rules, so this will need factoring in."
                    // I take this to mean that the operators in the expression must be in BEDMAS order
                    throw new CalculatorException("API does not support BEDMAS processing.");
                }
            }

            expression += operatorIn.ToString() + " ";
            lastOperator = operatorIn;

        }

        public double Value
        {
            get
            {
                double result = 0;

                result = GetAPIValue(expression).Result;
                return result;

            }

        }

        private static async Task<double> GetAPIValue(String expression)
        {
            double result = 0;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://api.mathjs.org/v4/");
                var response = await httpClient.GetAsync($"?expr={HttpUtility.UrlEncode(expression)}");

                // TODO: propogate the message in the response when the API returns an error
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                double.TryParse(body, out result);

            }

            return result;


        }


    }
}

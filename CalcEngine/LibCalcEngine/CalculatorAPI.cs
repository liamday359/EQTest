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

        public double Value(int? precision = null)
        {
            double result = 0;

            result = GetAPIValue(expression, precision).Result;
            return result;

        }

        private static async Task<double> GetAPIValue(String expression, int? precision)
        {
            double result = 0;

            using (HttpClient httpClient = new HttpClient())
            {
                // TODO : make this address configurable
                httpClient.BaseAddress = new Uri($"http://api.mathjs.org/v4/");
                String parameters = $"?expr={HttpUtility.UrlEncode(expression)}";
                if (precision.HasValue)
                {
                    parameters += $"&precision={precision.Value}";
                }
                var response = await httpClient.GetAsync(parameters);

                // TODO: propogate the message in the response when the API returns an error
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                double.TryParse(body, out result);

            }

            return result;


        }


    }
}

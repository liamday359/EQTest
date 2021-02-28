using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CalcClient
{
    class Program
    {
        private class ResponseData
        {
            public String Body { get; set; }
            public int StatusCode { get; set; }
        }

        static void Main(string[] args)
        {
            int? precision = null;
            int parsedprecision = 0;

            Console.WriteLine("Enter precision: (leave blank for no precision)");
            var enteredprecision = Console.ReadLine();
            if (int.TryParse(enteredprecision, out parsedprecision))
            {
                precision = parsedprecision;
            }

            do
            {
                Console.WriteLine("Enter expression: ");
                var expression = Console.ReadLine();

                var response = GetHttpResult(expression.Trim(), precision);
                response.Wait();
                Console.WriteLine(response.Result);
                Console.WriteLine("");

            } while (true);


        }

        private static async Task<String> GetHttpResult(String expression, double? precision)
        {
            String result = "";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"https://localhost:44380/api/CalcEngine/");
                String parameters = $"?expr={HttpUtility.UrlEncode(expression)}";
                if (precision.HasValue)
                {
                    parameters += $"&precision={precision.Value}";
                }

                var response = await httpClient.GetAsync(parameters);

                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = $"Result: {body}";
                }
                else
                {
                    result = $"Code {(int)response.StatusCode}: {body}";
                }

            }

            return result;

        }
    }
}

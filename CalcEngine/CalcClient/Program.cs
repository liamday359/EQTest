using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;

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

            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true).AddEnvironmentVariables();
            var config = builder.Build();
            var baseAddress = config["BaseAddress"];

            Console.WriteLine("Enter precision: [2]");
            var enteredprecision = Console.ReadLine();
            if (int.TryParse(enteredprecision, out parsedprecision))
            {
                precision = parsedprecision;
            }

            do
            {
                Console.WriteLine("Enter expression: ");
                var expression = Console.ReadLine();

                var response = GetHttpResult(baseAddress, expression.Trim(), precision);
                response.Wait();
                Console.WriteLine(response.Result);
                Console.WriteLine("");

            } while (true);


        }

        private static async Task<String> GetHttpResult(String baseAddress, String expression, double? precision)
        {
            String result = "";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseAddress);
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

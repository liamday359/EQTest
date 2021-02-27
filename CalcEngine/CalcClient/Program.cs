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
            do
            {
                Console.WriteLine("Enter expression (expression elements must be seperated by spaces): ");
                var expression = Console.ReadLine();

                var response = GetHttpResult(expression.Trim());
                response.Wait();
                Console.WriteLine(response.Result);
                Console.WriteLine("");

            } while (true);


        }

        private static async Task<String> GetHttpResult(String expression)
        {
            int code = 0;
            String result = "";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"https://localhost:44380/api/CalcEngine/");
                var response = await httpClient.GetAsync($"?expr={HttpUtility.UrlEncode(expression)}");

                code = (int)response.StatusCode;
                var body = await response.Content.ReadAsStringAsync();

                if (code >= 200 && code <= 299)
                {
                    result = $"Result: {body}";
                }
                else
                {
                    result = $"Code {code}: {body}";
                }

            }

            return result;


        }
    }
}

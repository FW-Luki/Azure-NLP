using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Amls
{
    class Program
    {
        static void Main(string[] args)
        {
            var ngrams = InvokeRequestResponseService().Result; ;

            Application.EnableVisualStyles();
            Application.Run(new Form1()
            {
                NGramCollection = new NGramCollection() { NGrams = ngrams.ToArray() }
            });
        }

        static async Task<IEnumerable<NGram>> InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                var apiKey = ConfigurationManager.AppSettings["apiKey"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["url"]);

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ResultContainer>(result);
                    Console.WriteLine("Result: {0}", result);

                    var ngrams = responseObject.Results.output1;
                    return ngrams.Select(ng => new NGram(ng));
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    Console.Read();

                    return Enumerable.Empty<NGram>();
                }
            }
        }
    }
}


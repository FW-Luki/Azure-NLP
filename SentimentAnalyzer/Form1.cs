using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SentimentAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static readonly object inputTtextSyncLock = new object();
        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                label1.Visible = false;
                label2.Visible = false;
                pictureBox1.Visible = true;
                label3.Enabled = false;
                try
                {
                    var sentiment = await InvokeRequestResponseService(textBox1.Text);
                    label1.Visible = sentiment?.Score == Sentiment.SentimentScore.High;
                    label2.Visible = sentiment?.Score == Sentiment.SentimentScore.Low;
                    pictureBox1.Visible = false;
                    label3.Visible = true;
                    label3.Enabled = true;
                    label3.Text = string.Format("{0:P2}", sentiment.Probability);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        static async Task<Sentiment> InvokeRequestResponseService(string text)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>()
                    {
                        {
                            "input1",
                            new List<Dictionary<string, string>>()
                            {
                                new Dictionary<string, string>()
                                {
                                    { "Col1", "high" },
                                    { "Col2", text }
                                }
                            }
                        },
                    },
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

                    //return responseObject.Results.output1.
                    return responseObject.Results.output1.Select(o => new Sentiment() { Score = (Sentiment.SentimentScore)Enum.Parse(typeof(Sentiment.SentimentScore), o.ScoredLabels, true), Probability = double.Parse(o.ScoredProbabilities) }).FirstOrDefault();
                }
                else
                {
                    var headers = response.Headers.ToString();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show(responseContent, headers, MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return null;
                }
            }
        }

    }
}

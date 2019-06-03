using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SentimentAnalyzer
{
    class Sentiment
    {
        public SentimentScore Score { get; set; }

        public double Probability { get; set; }


        public enum SentimentScore
        {
            High,
            Low
        }
    }


    public class Output1
    {
        public string Col1 { get; set; }

        [JsonProperty("Scored Labels")]
        public string ScoredLabels { get; set; }

        [JsonProperty("Scored Probabilities")]
        public string ScoredProbabilities { get; set; }
    }

    public class Results
    {
        public List<Output1> output1 { get; set; }
    }

    public class ResultContainer
    {
        public Results Results { get; set; }
    }
}

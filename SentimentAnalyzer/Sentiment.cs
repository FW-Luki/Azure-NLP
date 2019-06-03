using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SentimentAnalyzer
{
    /// <summary>
    /// Represents a positive (high) or negative (low) sentiment.
    /// </summary>
    public class Sentiment
    {
        /// <summary>
        /// Creates new instance of <see cref="Sentiment"/>.
        /// </summary>
        /// <param name="o">Azure Machine Learning Studio web-service specific response class containing information about the sentiment.</param>
        public Sentiment(Output1 o) : this((SentimentScore)Enum.Parse(typeof(SentimentScore), o.ScoredLabels, true), double.Parse(o.ScoredProbabilities))
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="Sentiment"/>.
        /// </summary>
        /// <param name="score">Sentiment score.</param>
        /// <param name="probability">Sentiment probability (certainty).</param>
        public Sentiment(SentimentScore score, double probability)
        {
            Score = score;
            Probability = probability;
        }


        /// <summary>
        /// Gets or sets this sentiment value.
        /// </summary>
        public SentimentScore Score { get; }

        /// <summary>
        /// Gets or sets probability (certainty) of this sentiment.
        /// </summary>
        public double Probability { get; }


        /// <summary>
        /// Sentiment value.
        /// </summary>
        public enum SentimentScore
        {
            /// <summary>
            /// Represents positive sentiment value.
            /// </summary>
            High,

            /// <summary>
            /// Represents negative sentiment value.
            /// </summary>
            Low
        }
    }


    #region Azure Machine Learning Studio web-service specific response classes
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    #endregion
}

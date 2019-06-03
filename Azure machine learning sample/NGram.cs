using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amls
{
    /// <summary>
    /// Represents an n-gram.
    /// </summary>
    public class NGram
    {
        /// <summary>
        /// Creates new instance of <see cref="NGram"/>.
        /// </summary>
        /// <param name="o">Azure Machine Learning Studio web-service specific response class containing information about the n-gram.</param>
        public NGram(Output1 o) : this(int.Parse(o.Id), o.Ngram, int.Parse(o.DF), double.Parse(o.IDF))
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="NGram"/>.
        /// </summary>
        /// <param name="id">N-gram id.</param>
        /// <param name="value">Actual n-gram value.</param>
        /// <param name="df">DF value.</param>
        /// <param name="idf">IDF value.</param>
        public NGram(int id, string value, int df, double idf)
        {
            Id = id;
            Value = value;
            DF = df;
            IDF = idf;

            Words = Value.Split(new[] { '_' });
            N = Words.Count();
        }


        /// <summary>
        /// N-gram id in a response list.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Actual n-gram value. Contains words separated by underscore '_'.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// DF value.
        /// </summary>
        public int DF { get; }

        /// <summary>
        /// IDF value.
        /// </summary>
        public double IDF { get; }


        /// <summary>
        /// Gets a collectionof words contained by this n-gram.
        /// </summary>
        public IEnumerable<string> Words { get; }

        /// <summary>
        /// Gets the n-gram rank - number of words contained by the n-gram.
        /// </summary>
        public int N { get; }
    }


    #region Azure Machine Learning Studio web-service specific response classes
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ResultContainer
    {
        public Results Results { get; set; }
    }

    public class Results
    {
        public List<Output1> output1 { get; set; }
    }

    public class Output1
    {
        public string Id { get; set; }
        public string Ngram { get; set; }
        public string DF { get; set; }
        public string IDF { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    #endregion
}

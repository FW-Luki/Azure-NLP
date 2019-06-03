using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amls
{
    public class ResultContainer
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
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


    public class NGram
    {
        public NGram(Output1 o) : this(int.Parse(o.Id), o.Ngram, int.Parse(o.DF), double.Parse(o.IDF))
        {
        }

        public NGram(int id, string value, int df, double idf)
        {
            Id = id;
            Ngram = value;
            DF = df;
            IDF = idf;

            Words = Ngram.Split(new[] { '_' });
            N = Words.Count();
        }

        public int Id { get; }
        public string Ngram { get; }
        public int DF { get; }
        public double IDF { get; }

        public IEnumerable<string> Words { get; }
        public int N { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amls
{
    public class NGramCollection
    {
        private const int MaxN = 3;

        public NGram[] NGrams { get; set; }

        public IEnumerable<string> GetHints(IEnumerable<string> contextWords)
        {
            return GetHintsInternal(contextWords).Distinct();
        }

        private IEnumerable<string> GetHintsInternal(IEnumerable<string> contextWords)
        {
            var recentWord = contextWords.Last();

            if (!string.IsNullOrWhiteSpace(recentWord))
            {
                foreach (var unigram in NGrams.Where(ng => ng.N == 1))
                {
                    var unigramWord = unigram.Words.Single();
                    if (unigramWord.StartsWith(contextWords.Last()))
                    {
                        yield return unigramWord;
                    }
                }
            }

            foreach (var ngram in NGrams.Where(ng => ng.N > 1).OrderByDescending(ng => ng.N))
            {
                var nonEmptyContextWords = contextWords.Where(w => !string.IsNullOrWhiteSpace(w));
                if (Enumerable.SequenceEqual(ngram.Words.Take(ngram.N - 1), nonEmptyContextWords.Skip(nonEmptyContextWords.Count() - (ngram.N - 1))))
                {
                    yield return ngram.Words.Last();
                }
            }

            //for (int n = Math.Min(wordCount, MaxN); n >= 1; n--)
            //{
            //    foreach (var ngram in NGrams.Where(ng => ng.Words.Count() == n))
            //    {
            //        if (ngram.Words.Last().StartsWith(contextWords.Last()) && !string.IsNullOrWhiteSpace(contextWords.Last()))
            //        {
            //            result.Add(ngram.Words.Last());
            //        }

            //        //car insurance policy buy 
            //        if (Enumerable.SequenceEqual(ngram.Words.Take(ngram.Words.Count() - 1), contextWords.Skip(contextWords.Count() - n - 1).Take(n - 1)))
            //        {
            //            result.Add(ngram.Words.Last());
            //        }
                    
            //    }
            //}
        }
    }
}

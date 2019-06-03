using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amls
{
    /// <summary>
    /// Contains a collection of <see cref="NGram"/>.
    /// Used for storing n-grams and getting relevant hints from the collection based on provided context.
    /// </summary>
    public class NGramCollection
    {
        private const int MaxN = 3;

        /// <summary>
        /// Gets or sets the array of <see cref="NGram"/>.
        /// </summary>
        public NGram[] NGrams { get; set; }

        /// <summary>
        /// Returns a collection of words relevant to provided context.
        /// </summary>
        /// <param name="contextWords">Collection of words representing context.</param>
        /// <returns>An IEnumerable of <see cref="string"/>.</returns>
        public IEnumerable<string> GetHints(IEnumerable<string> contextWords)
        {
            return GetHintsInternal(contextWords).Distinct();
        }

        private IEnumerable<string> GetHintsInternal(IEnumerable<string> contextWords)
        {
            //Get unigrams for autocomplete last typed word.
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

            //Get (n>1)-grams for hints containing words relevant to the context.
            foreach (var ngram in NGrams.Where(ng => ng.N > 1).OrderByDescending(ng => ng.N))
            {
                var nonEmptyContextWords = contextWords.Where(w => !string.IsNullOrWhiteSpace(w));
                if (Enumerable.SequenceEqual(ngram.Words.Take(ngram.N - 1), nonEmptyContextWords.Skip(nonEmptyContextWords.Count() - (ngram.N - 1))))
                {
                    yield return ngram.Words.Last();
                }
            }
        }
    }
}

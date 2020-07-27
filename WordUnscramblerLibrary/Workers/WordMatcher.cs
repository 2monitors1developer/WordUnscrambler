using System;
using System.Collections.Generic;
using WordUnscramblerLibrary.Models;

namespace WordUnscramblerLibrary.Workers
{
    public class WordMatcher
    {
        public List<MatchedWordModel> Match(string[] scrambledWords, string[] wordList)
        {
            var output = new List<MatchedWordModel>();

            foreach (string scrambledWord in scrambledWords)
            {
                foreach (string word in wordList)
                {
                    if (scrambledWord.Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        output.Add(BuildMatchedWord(scrambledWord, word));
                    }
                    else
                    {
                        var scrambledWordArray = scrambledWord.ToCharArray();
                        var wordArray = word.ToCharArray();

                        Array.Sort(scrambledWordArray);
                        Array.Sort(wordArray);

                        var sortedScrambledWord = new string(scrambledWordArray);
                        var sortedWord = new string(wordArray);

                        if (sortedScrambledWord.Equals(sortedWord, StringComparison.OrdinalIgnoreCase))
                        {
                            output.Add(BuildMatchedWord(scrambledWord, word));
                        }
                    }
                }
            }

            return output;
        }

        private MatchedWordModel BuildMatchedWord(string scrambledWord, string word)
        {
            var matchedWord = new MatchedWordModel
            {
                ScrambledWord = scrambledWord,
                Word = word
            };

            return matchedWord;
        }
    }
}

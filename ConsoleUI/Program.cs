using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscramblerLibrary.Models;
using WordUnscramblerLibrary.Workers;

namespace ConsoleUI
{
    internal class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();

        static void Main()
        {
            try
            {
                bool continueWordUnscramble;
                do
                {
                    Console.Write(Constants.OptionsHowToEnterWords);
                    var option = Console.ReadLine() ?? string.Empty;

                    switch (option.ToUpper())
                    {
                        case Constants.File:
                            Console.Write(Constants.EnterWordsViaFile);
                            ExecuteWordsInFileScenario();
                            break;
                        case Constants.Manual:
                            Console.Write(Constants.EnterWordsManually);
                            ExecuteWordsManuallyScenario();
                            break;
                        default:
                            Console.Write(Constants.EnterWordsOptionNotRecognised);
                            break;
                    }

                    string continueResult;
                    do
                    {
                        Console.Write(Constants.OptionsToContinue);
                        continueResult = Console.ReadLine() ?? string.Empty;

                    } while (!continueResult.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                             !continueResult.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

                    continueWordUnscramble = continueResult.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordUnscramble);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ Constants.ErrorProgramWillBeTerminated }: { ex.Message }");
            }
        }

        private static void ExecuteWordsInFileScenario()
        {
            try
            {
                var filename = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read(filename);
                DisplayMatchedWords(scrambledWords);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ Constants.ErrorWordsCannotBeLoaded}: { ex.Message }");
            }
        }

        private static void ExecuteWordsManuallyScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedWords(scrambledWords);
        }

        private static void DisplayMatchedWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordListFileName);

            List<MatchedWordModel> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine(Constants.MatchFound, matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.MatchNotFound);
            }
        }
    }
}

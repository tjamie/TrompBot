using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrompBot_Console
{
    static class Modify
    {
        static char[] lowerVowels = { 'a', 'i', 'u' };
        static char[] upperVowels = { 'A', 'I', 'U' };

        static char[] lowerReps = { 'e', 'o' };
        static char[] upperReps = { 'E', 'O' };

        #region case-sensitive exclusionDictionary
        //string[] exclusionDictionary =
        //{
        //    "United States",
        //    "America",
        //    "China",
        //    "Japan",
        //    "Korea",
        //    "Germany",
        //    "Syria",
        //    "Hillary",
        //    "Obama",
        //    //short words
        //    "I",
        //    "a",
        //    "A",
        //    "it",
        //    "all"
        //};
        #endregion

        static string[] exclusionDictionary =
        {
                //countries and names
                "america",
                "america.",
                "america,",
                "america!",
                "u.s.",
                "american",
                "americans",
                "china",
                "japan",
                "germany",
                "syria",
                "hillary",
                "obama",
                //short words
                "i",
                "i,",
                "a",
                "at",
                "as",
                "it",
                "if",
                "is",
                "all",
                "ill",
                "in",
                "and", //might want to replace with something like "nd" or "+" instead
                //symbols
                "&",
                //"&amp",
                //"&amp,",
                //"&amp;",
                //alphabet agencies
                "cia",
                "fbi",
                "epa",
                "irs",
                //etc
                "media",
                "media.",
                "media,",
                "used"
            };

        //[string to replace, replacement string]
        static string[,] replacementDictionary =
        {
                //SMSification and general ignorance of English
                {"your", "ur" },
                {"you're", "ur" },
                {"you", "u" },
                {"YOU", "U" },
                {"You", "U" },
                {"love", "<3" },
                {"to", "2" },
                {"too", "to" },
                {"two", "too" },
                {"for", "4" },
                {"their", "there" },
                {"they're", "there" },
                {"there's", "thers" },
                {"though", "tho" },
                {"although", "altho" },
                {"heal", "heel" },
                {"fake", "faek" },
                {"Fake", "Faek" },
                {"fight", "fite" },
                {"pure", "pyur" },
                //antonyms
                {"humble", "rude" },
                {"GREAT", "BAD" },
                //formatting corrections
                {"&amp;", "&" },
                //etc
                {"JOBS", "JERB" },
                {"JOBS,", "JERB," },
                {"coverage", "covfefe" },
                {"one", "uno" }
            };

        public static string ModifyTweet(string[] tweet)
        {
            bool exclusionFound;

            //divide each word into an array to modify individual characters, then join

            for (int iWord = 0; iWord < tweet.Length; iWord++)
            {
                exclusionFound = CheckExclusion(tweet[iWord]);

                if (!exclusionFound)
                {
                    string replacement = ReplaceWord(tweet[iWord]);
                    if (replacement != null)
                    {
                        tweet[iWord] = replacement;
                    }
                    else
                    {
                        tweet[iWord] = ReplaceVowels(tweet[iWord]);
                    }
                }
            }
            //recombine modified words
            string newTweet = string.Join(" ", tweet);
            return newTweet;
        }
        
        static bool CheckExclusion(string word)
        {
            if (exclusionDictionary.Contains(word.ToLower()))
            {
                return true;
            }
            // Keeps URLs, @Handles, and #Hashtags in tact
            else if (word.Contains("http") || word.Contains('#') || word.Contains('@'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static string ReplaceWord(string word)
        {
            //detect words to be replaced, if present
            for (int iCheck = 0; iCheck < replacementDictionary.GetLength(0); iCheck++)
            {
                if (word == replacementDictionary[iCheck, 0])
                {
                    return replacementDictionary[iCheck, 1];
                }
            }

            return null;
        }

        static string ReplaceVowels(string word)
        {
            //examine each letter in current word, replace if applicable
            char[] chars = word.ToCharArray();

            try
            {
                for (int iLetter = 0; iLetter < chars.Length; iLetter++)
                {
                    if (lowerVowels.Contains(chars[iLetter]))
                    {
                        //replace vowel with a random of 'e' or 'o'
                        chars[iLetter] = lowerReps[Number.RNG(0, lowerReps.Length - 1)];
                    }
                    else if (upperVowels.Contains(chars[iLetter]))
                    {
                        chars[iLetter] = upperReps[Number.RNG(0, upperReps.Length - 1)];
                    }
                }
                return string.Join("", chars);
            }
            catch (System.IndexOutOfRangeException ex)
            {
                string lol = "covfefe";
                Console.WriteLine("[{0}] Exception thrown: {1}\n     Replacing with {2}.", Timestamp.GetTime(), ex.Message, lol);
                return lol;
            }
        }
    }
}

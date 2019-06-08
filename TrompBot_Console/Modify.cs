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
        static Dictionary<string, string> replacementDict = new Dictionary<string, string>
        {
            //SMSification and general ignorance of English
            {"your", "ur" },
            {"you're", "ur" },
            {"you", "u" },
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
            {"fight", "fite" },
            {"pure", "pyur" },
            {"origin", "orange" },
            {"origins", "oranges" },
            //antonyms
            {"humble", "rude" },
            {"great", "bad" },
            //formatting corrections
            {"&amp;", "&" },
            //etc
            {"jobs", "jerb" },
            {"coverage", "covfefe" },
            {"one", "uno" }
        };
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
            // detect words to be replaced, if present
            // Note: input string may contains characters other than a-Z, eg, \"
            //for (int iCheck = 0; iCheck < replacementDictionary.GetLength(0); iCheck++)
            //{
            //    if (word == replacementDictionary[iCheck, 0])
            //    {
            //        return replacementDictionary[iCheck, 1];
            //    }
            //}
            //return null;

            char[] wordArr = word.ToArray();
            
            string sansPunctuation = new string(wordArr.Where(c => !char.IsPunctuation(c)).ToArray());

            if (word == "&amp;")
            {
                return "&";
            }
            else if (replacementDict.ContainsKey(word.ToLower()) || replacementDict.ContainsKey(sansPunctuation.ToLower()))
            {
                // replace word
                // word = replacementDict[sansPunctuation];
                string wordLower = word.ToLower();
                string newWord = wordLower.Replace(sansPunctuation.ToLower(), replacementDict[sansPunctuation.ToLower()]);
                // TODO: preserve letter case

                return newWord;
                #region idk
                // TODO: return to this and implement capitalization preservation... this was just hacked together to put it in a somewhat
                // workable state to fix code elsewhere

                // below is dumb
                //// return if any numbers present in new word (eg <3, 2, 4)
                //if (word.Any(char.IsDigit))
                //{
                //    return word;
                //}
                //else
                //{
                //    // reapply letter case
                //    int j = 0;
                //    char[] newWordArr = word.ToArray();
                //    for (int i = 0; i < newWordArr.Length; i++)
                //    {
                //        if (char.IsLower(wordArr[j]))
                //        {
                //            newWordArr[i] = char.ToLower(newWordArr[i]);
                //        }
                //        else if (char.IsUpper(wordArr[j]))
                //        {
                //            newWordArr[i] = char.ToUpper(newWordArr[i]);
                //        }
                //        else
                //        {
                //            // ie, char is a digit or punctuation
                //            j++;
                //        }
                //        j++;
                //    }

                //    // reapply punctuation if present
                //    // I guess we could just add non-letter characters to start/end of newWordArr?
                //    // j for index of original input, i for index of word from replacement dictionary

                //    // **TODO: think of a better way to do this because the below is retarded**

                //    j = 0;
                //    int iMid;
                //    if (wordArr.Length - newWordArr.Length > 0)
                //    {
                //        char[] trimmedChars = new char[wordArr.Length - newWordArr.Length];
                //        for (int i = 0; i < wordArr.Length; i++)
                //        {
                //            // Compare characters (ToLower)
                //            while (char.ToLower(wordArr[j]) != char.ToLower(newWordArr[i]))
                //            {
                //                trimmedChars[j] = wordArr[j];
                //                j++;
                //            }
                //            // set point at which the last front character is located in trimmedChars
                //            iMid = j - 1;
                //            break; // because I'm too lazy to just delete the for
                //        }
                //        // Then when letters match:
                //        // Get characters trimmed from end of input

                //        // TODO: only run below if necessary

                //        j = wordArr.Length - 1;
                //        for (int i = wordArr.Length - 1; i >= 0; i--)
                //        {
                //            while (char.ToLower(wordArr[j]) != char.ToLower(newWordArr[newWordArr.Length - 1]))
                //            {
                //                // too dumb to do this right now
                //            }
                //        }

                //    }
                //    else // (no stripped characters present)
                //    {
                //        return newWordArr.ToString();
                //    }
                //}
                #endregion
            }
            else
            {
                return ReplaceVowels(word);
            }
        }

        static string ReplaceVowels(string word)
        {
            //examine each letter in current word, replace if applicable
            char[] chars = word.ToCharArray();

            int[] binaryArr = Number.RandomBinary();

            try
            {
                for (int iLetter = 0; iLetter < chars.Length; iLetter++)
                {
                    if (lowerVowels.Contains(chars[iLetter]))
                    {
                        //replace vowel with a random of 'e' or 'o'
                        chars[iLetter] = lowerReps[binaryArr[iLetter % binaryArr.Length]];
                        //chars[iLetter] = lowerReps[Number.RNG(0, lowerReps.Length - 1)];
                    }
                    else if (upperVowels.Contains(chars[iLetter]))
                    {
                        chars[iLetter] = upperReps[binaryArr[iLetter % binaryArr.Length]];
                        //chars[iLetter] = upperReps[Number.RNG(0, upperReps.Length - 1)];
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

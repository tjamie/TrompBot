using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrompBot_Console
{
    class Modify
    {
        public static string ModifyTweet(string[] tweet)
        {
            //exclude o and e for now
            char[] lowerVowels = { 'a', 'i', 'u' };
            char[] upperVowels = { 'A', 'I', 'U' };

            char[] lowerReps = { 'e', 'o' };
            char[] upperReps = { 'E', 'O' };

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

            string[] exclusionDictionary =
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
            string[,] replacementDictionary =
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
            //Console.WriteLine("RepDic rows: {0}", replacementDictionary.GetLength(0));

            //divide each word into an array to modify individual characters, then join
            //Note: tweet is entering this method as an array of words (or whatever you want to call what Donnie spews out)

            for (int iWord = 0; iWord < tweet.Length; iWord++)
            {
                bool exclusionFound = false;

                //bool placeHolderBool = false;
                if (exclusionDictionary.Contains(tweet[iWord].ToLower()))
                {
                    //do nothing (except set bool)
                    //Again, there's probably a much better way to handle this than using a bool, but I'm tired and such
                    exclusionFound = true;
                }
                else if (!exclusionFound)
                {
                    //detect words to be replaced, if present
                    for (int iCheck = 0; iCheck < replacementDictionary.GetLength(0); iCheck++)
                    {
                        if (tweet[iWord] == replacementDictionary[iCheck, 0])
                        {
                            tweet[iWord] = replacementDictionary[iCheck, 1];
                            exclusionFound = true;
                        }
                    }
                }
                else if (tweet[iWord].Contains("http"))
                {
                    exclusionFound = true;
                }
                //else if (tweet[iWord].Contains('#'))
                //{
                //    exclusionFound = true;
                //    //redundancy; apparently the following # check can fail
                //    //doesn't help multiple hashtags, but also doesn't crash
                //}

                if (!exclusionFound)
                {
                    //Check the character array of each word/string
                    //string currentWord = tweet[iWord];
                    char[] chars = tweet[iWord].ToCharArray();

                    //detect hashtag
                    //if (chars[0] == '#')
                    //{
                    //    //(do nothing, there's probably a better way to do this but whatever)
                    //    out of range exception for some fucking reason
                    //}
                    //else if (chars.Contains('#'))

                    //Let's hope this works
                    try
                    {
                        if (chars.Contains('#'))
                        {
                            //do nothing
                            //redundancy
                            //Do line breaks break things?
                        }
                        else if (chars[0] == '@')
                        {
                            //still do nothing, still probably a better way to do this

                            //System.IndexOutOfRangeException: 'Index was outside the bounds of the array.'

                            //Broken by:
                            /**
                                Tainted (no, very dishonest?) FBI “agent’s role in Clinton probe under review.” Led Clinton Email probe. @foxandfriends Clinton money going to wife of another FBI agent in charge.
                             
                            **/

                        }
                        else
                        {
                            //examine each letter in current word, replace if applicable
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
                        }
                    }
                    catch(System.IndexOutOfRangeException ex)
                    {
                        Console.WriteLine("[{0}] Exception thrown: {1}", TrompBot_Console.Timestamp.GetTime(), ex.Message);
                    }
                    

                    //recombine characters
                    tweet[iWord] = string.Join("", chars);
                }
            }
            //recombine modified words
            string newTweet = string.Join(" ", tweet);
            return newTweet;
        }

        static void splitTweet(string longTweet, string[] multiTweet)
        {
            //split long tweet into arrays
            string currentTweet = "";
            int i = 0;

            string[] tweetArray = longTweet.Split(' ');
            List<string> tweetBuilder = new List<string>();
            int wordCount = tweetArray.Length;

            //add words to sub-tweet until characters exceed 140
            while (currentTweet.Length < 140)
            {
                //To be continued. I'm too tired for this.
            }
        }
    }
}

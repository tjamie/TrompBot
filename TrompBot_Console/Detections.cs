using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrompBot_Console
{
    class Detections
    {
        public static bool IsRetweet(string tweetString)
        {
            char[] tweetChars = tweetString.ToCharArray();
            
            if (tweetChars[0] == 'R' && tweetChars[1] == 'T' && tweetChars[2] == ' ')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDuplicate(string newTweet, string prevTweet)
        {
            //TODO: expand to store previous 5 tweets or so
            if (newTweet == prevTweet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

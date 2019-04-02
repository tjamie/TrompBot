using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrompBot_Console
{
    class ReplyLogic
    {
        static void GetReply()
        {
            //reply to mentions of @realDonaldTrump
            //2d array: key word(s) in first column, reply in second column (possibly send reply through ModifyTweet() before publishing)
            //If no key words found, reply with random statement from a separate array

            //keep reply text to 123 characters or less (140-17 to allow room for max screen name size + " ")

            //use str.ToLower() when finding matches in arrays

            string[,] triggerDictionary =
            {
                {"small hands", "What did you just say? My hands are the greatest! Bigly!" }, //56
                {"hands", "Have you seen my hands? They're gigantic!" }
            };

            string[] replyDictionary =
            {
                "Placeholder1",
                "Placeholder2"
            };
        }
    }
}

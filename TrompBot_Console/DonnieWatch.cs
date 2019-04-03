using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Exceptions;

namespace TrompBot_Console
{
    class DonnieWatch
    {
        public static void Dotard()
        {
            //Twitter user IDs
            //Tromp ID: 918502759573450752
            //Trump ID: 25073877
            int targetID = 25073877;
            string lastTweet = "";

            int tweetLength;
            const int maxCharacterCount = 280;

            Console.Write(Environment.NewLine);
            Console.WriteLine("[{0}] Starting DonnieWatch() - Target Twitter ID: {1}", Timestamp.GetTime(), targetID);
            Console.Write(Environment.NewLine);

            var stream = Stream.CreateFilteredStream();

            stream.AddFollow(targetID);

            stream.MatchingTweetReceived += (sender, args) =>
            {
                string tweetString;
                string[] tweetArray;
                string newTweet;
                bool modTweet;
                tweetString = Convert.ToString(args.Tweet);

                if (!Detections.IsRetweet(tweetString) && !Detections.IsDuplicate(tweetString, lastTweet))
                {
                    tweetArray = TweetToArray(tweetString);

                    if (tweetString.ToLower().Contains("@realdonaldtrump") || tweetString.ToLower().Contains("@reeldonoldtromp"))
                    {
                        modTweet = false;
                    }
                    else
                    {
                        modTweet = true;
                    }

                    if (modTweet == true)
                    {
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("[{0}] Tweet detected: \"{1}\"", Timestamp.GetTime(), tweetString);
                        System.Threading.Thread.Sleep(5000);

                        newTweet = Modify.ModifyTweet(tweetArray);

                        tweetLength = newTweet.Length;

                        Console.WriteLine("[{0}] Character count: {1}", Timestamp.GetTime(), tweetLength);

                        if (tweetLength <= maxCharacterCount)
                        {
                            Console.WriteLine("[{0}] Tweet to be published: {1}", Timestamp.GetTime(), newTweet);

                            #region ExceptionHandler
                            //try catch adopted from: https://github.com/linvi/tweetinvi/wiki/Exception-Handling#per-request-exception-handling
                            ExceptionHandler.SwallowWebExceptions = false;
                            try
                            {
                                Tweet.PublishTweet(newTweet);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine("[{0}] Request parameters invalid: \"{1}\"", Timestamp.GetTime(), ex.Message);
                            }
                            catch (TwitterException ex)
                            {
                                //failed API request/ bad request, network failure, or unauthorized request
                                Console.WriteLine("[{0}] Failed HTTP request: \"{1}\"", Timestamp.GetTime(), ex.TwitterDescription);
                            }
                            #endregion

                            Console.WriteLine("[{0}] Tweet publishing attempted.", Timestamp.GetTime());
                            lastTweet = tweetString;
                        }
                        else
                        {
                            Console.WriteLine("[{0}] Character count exceeds {1}. Will not publish.\n   Tweet: {2}", Timestamp.GetTime(), maxCharacterCount, newTweet);
                        }
                    }
                }
                else
                {
                    if (!tweetString.ToLower().Contains("@realdonaldtrump"))
                    {
                        Console.WriteLine("[{0}] Retweet (or duplicate) detected -- will not publish.\n     \"{1}\"", Timestamp.GetTime(), tweetString);
                    }
                }
            };

            // In case of disconnects:
            stream.StreamStopped += (sender, args) =>
            {
                stream.StartStreamMatchingAllConditions();
            };

            stream.StartStreamMatchingAllConditions();
        }
        static string[] TweetToArray(string tweet) => tweet.Split(' ');
    }
}

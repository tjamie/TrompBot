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
            //Twitter user IDs, testing and running
            //string ID = "25073877";
            //Tromp ID: 918502759573450752
            //long targetID = 918502759573450752;
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
                tweetString = Convert.ToString(args.Tweet);

                if (!TrompBot_Console.Detections.IsRetweet(tweetString) && !TrompBot_Console.Detections.IsDuplicate(tweetString, lastTweet))
                {
                    //Console.WriteLine("String: {0}", tweetString);
                    bool modTweet = true;
                    tweetArray = TweetToArray(tweetString);
                    foreach (string word in tweetArray)
                    {
                        //if (word.ToLower() == "@realdonaldtrump" || word.ToLower() == "@realdonaldtrump." || word.ToLower() == "@realdonaldtrump?" || word.ToLower() == "@realdonaldtrump," || word.ToLower() == "@realdonaldtrump!")
                        if (word.ToLower().Contains("@realdonaldtrump") || word.ToLower().Contains("@reeldonoldtromp"))
                        {
                            modTweet = false;
                        }
                    }

                    if (modTweet == true)
                    {
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("[{0}] Tweet detected: \"{1}\"", Timestamp.GetTime(), tweetString);
                        System.Threading.Thread.Sleep(5000);

                        #region debug: array dump
                        //Console.WriteLine("Array:");
                        //for (int i = 0; i < tweetArray.Length; i++)
                        //{
                        //    Console.Write("({0}){1} ", i, tweetArray[i]);
                        //}
                        //Console.Write(Environment.NewLine);
                        #endregion

                        newTweet = TrompBot_Console.Modify.ModifyTweet(tweetArray);
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
                            //TODO: finish Modify.SplitTweet to publish multiple tweets
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

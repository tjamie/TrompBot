using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Exceptions;

namespace TrompBotCS
{
    class Program
    {
        /** TODO:
         * ============== BEFORE PUBLISHING ==============
         * 
         * Add a delay between tweets, or a maximum number of tweets published within a five or ten minute period... Let's not get banned again
         *  (BandAid applied -- Will no longer publish tweets with "RT" detected)
         *
         * Also need to do a large amount of garbage collection once we're running again... parts of this have gotten fugly
         * 
         **/

        //TODO: separate classes into different .cs files... I didn't expect this to get as big as it is
        
        static void Main(string[] args)
        {
            const string version = "0.21";

            #region Authentication Information
            //authentication
            //TODO: redact credentials if source code ever gets released, move credentials to another class (including google cloud/whatever if that ever gets implemented)
            string consumerKey = TrompBot_Console.TrompCredentials.consumerKey;
            string consumerSecret = TrompBot_Console.TrompCredentials.consumerSecret;
            string accessToken = TrompBot_Console.TrompCredentials.accessToken;
            string accessTokenSecret = TrompBot_Console.TrompCredentials.accessTokenSecret;

            //Auth.SetUserCredentials("0Y5IMAKuflHM5bo9kS3K1zoIl", "1vA6zuEvAGhGMuZJMU4Sjf01zfXaqkTi4JrLBTjt1bmbsax1fK", "918502759573450752-e0G93Zanadk7eNXmLlZaFCH9IaDt3sr", "IrV4FWupjuwjAu272kNVTfmnKIgj1Ona2wvS6gA7CqPbn");
            //CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET
            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            
            #endregion

            var authenticateUser = User.GetAuthenticatedUser();

            bool exit = false;

            //set directory to default
            CSV.DefaultStart();

            while (!exit)
            {
                bool proceed = false;

                Console.WriteLine("Welcome to TrompBot version {0}. Current environment: {1}", version, Environment.OSVersion);
                Console.WriteLine("Authenticating user...");
                Console.WriteLine("User authenticated: {0}", authenticateUser);
                Console.Write(Environment.NewLine);

                Console.WriteLine("Consumer Key: {0}", consumerKey);
                Console.WriteLine("Consumer Secret: {0}", consumerSecret);
                Console.WriteLine("Access Token: {0}", accessToken);
                Console.WriteLine("Access Token Secret: {0}", accessTokenSecret);
                Console.Write(Environment.NewLine);

                Console.WriteLine("Current directory: {0}", CSV.directory);
                Console.Write(Environment.NewLine);

                while (!proceed)
                {
                    Console.WriteLine("Enter command.");
                    Console.Write(Environment.NewLine);

                    string input = Console.ReadLine();
                    string[] argv = input.Split(' ');
                    switch (argv[0].ToLower())
                    {
                        case "start":
                            bool DonLock = true;
                            while (DonLock)
                            {
                                TrompBot_Console.DonnieWatch.Dotard();
                            }
                            break;
                        case "tweet":
                            if (argv.Length < 2)
                            {
                                Console.WriteLine("Invalid input - no arguments");
                            }
                            else
                            {
                                //TweetMessage(argv);
                                string garbageOut = input.Remove(0, 6);
                                Tweet.PublishTweet(garbageOut);
                                Console.WriteLine("[{0}] Test tweet publish attempted -- \"{1}\"", TrompBot_Console.Timestamp.GetTime(), garbageOut);
                            }
                            break;
                        case "dir":
                            CSV.Start(argv);
                            break;
                        case "modtest":
                            Console.WriteLine(TrompBot_Console.Modify.ModifyTweet(argv));
                            break;
                        case "rngtest":
                            {
                                int min = Convert.ToInt32(argv[1]);
                                int max = Convert.ToInt32(argv[2]);
                                Console.WriteLine(TrompBot_Console.Number.RNG(min, max));
                                break;
                            }
                        case "rngloop":
                            {
                                int min = Convert.ToInt32(argv[1]);
                                int max = Convert.ToInt32(argv[2]);
                                for (int i = 0; i < 10; i++)
                                {
                                    Console.WriteLine("({0}) {1}", i, TrompBot_Console.Number.RNG(min, max));
                                    //System.Threading.Thread.Sleep(10);
                                }
                                break;
                            }
                        case "testlist":
                            {
                                TrompBot_Console.Tests.ListTest();
                                break;
                            }
                        case "translate":
                            {
                                string transcmd = input.Substring(argv[0].Length + 1);
                                Console.WriteLine("Starting translator with command: {0}", transcmd);
                                TrompBot_Console.Translate.Start(input);
                                //TrompBot_Console.Translate.Load();
                                break;
                            }
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }

        }
    }
    
    class Notes
    {
        /*
         
        Publish tweet:
            Tweet.PublishTweet("Message");

        .csv format:
            Date,Time,Tweet
            d MMM yyyy,HH:mm:ss,"Tweet text"

        Failed tweets:
            My visit to Japan and friendship with PM Abe will yield many benefits, for our great Country. Massive military & energy orders happening+++!


        */
                    }
                }

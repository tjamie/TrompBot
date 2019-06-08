using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Exceptions;

namespace TrompBot_Console
{
    // TODO: de-static everything
    class Program
    {
        static void Main(string[] args)
        {
            const string version = "0.24";

            #region Authentication Information
            //authentication
            //string consumerKey = TrompBot_Console.TrompCredentials.consumerKey;
            //string consumerSecret = TrompBot_Console.TrompCredentials.consumerSecret;
            //string accessToken = TrompBot_Console.TrompCredentials.accessToken;
            //string accessTokenSecret = TrompBot_Console.TrompCredentials.accessTokenSecret;
            TrompCredentials.Credentials creds = TrompCredentials.GetCredentials();
            string consumerKey = creds.consumerKey;
            string consumerSecret = creds.consumerSecret;
            string accessToken = creds.accessToken;
            string accessTokenSecret = creds.accessTokenSecret;

            Console.WriteLine("Credentials received:\n Consumer Key: {0}\n Consumer Secret: {1}\n Access Token: {2}\n Access Token Secret: {3}", consumerKey, consumerSecret, accessToken, accessTokenSecret);
            
            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            
            #endregion

            //var authenticateUser = User.GetAuthenticatedUser();

            bool exit = false;

            //set directory to default
            //CSV.DefaultStart();

            while (!exit)
            {
                bool proceed = false;

                Console.WriteLine("Welcome to TrompBot version {0}. Current environment: {1}", version, Environment.OSVersion);
                Console.WriteLine("Authenticating user...");
                //Console.WriteLine("User authenticated: {0}", authenticateUser);
                var authenticateUser = User.GetAuthenticatedUser();
                Console.Write(Environment.NewLine);

                //Console.WriteLine("Consumer Key: {0}", consumerKey);
                //Console.WriteLine("Consumer Secret: {0}", consumerSecret);
                //Console.WriteLine("Access Token: {0}", accessToken);
                //Console.WriteLine("Access Token Secret: {0}", accessTokenSecret);
                //Console.Write(Environment.NewLine);

                //Console.WriteLine("Current directory: {0}", CSV.directory);
                //Console.Write(Environment.NewLine);

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
                        case "jsontest":
                            {
                                TrompBot_Console.Tests.GetCreds();
                                break;
                            }
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
}

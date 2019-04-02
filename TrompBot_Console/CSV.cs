using System;
using System.Collections.Generic;
using System.Text;

namespace TrompBotCS
{
    public class CSV
    {
        //directory stuff
        //I'm guessing this only works with Windows, so:
        //TODO: linux compatibility
        static readonly char[] invalidChars = { '*', '[', ']', '"', ':', ';', '=', ',', '&', '#', '/' };

        public static string directory;
        public static string directoryWithFile;
        public static string defaultDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TrompBot");
        public static string temporaryDirectory;
        public static bool directorySet = false;

        static readonly string fileName = "TrompBotLog.csv";

        public static void Reset()
        {
            directory = defaultDirectory;
            temporaryDirectory = string.Empty;
            directorySet = false;
        }

        public static void DefaultStart()
        {
            directory = defaultDirectory;
        }

        public static void Start(string[] argv)
        {
            switch (argv[1].ToLower())
            {
                case "writetest":
                    {
                        string test = "This is not a tweet";
                        Write(test);
                        break;
                    }
                case "set":
                    {
                        if (argv.Length < 3)
                        {
                            Console.WriteLine("Invalid argument. Did you mean \"dir set C:\\FilePath\"?");
                        }
                        else if (argv[2] == "default")
                        {
                            directory = defaultDirectory;
                            directorySet = true;
                            Console.WriteLine("Directory set to {0}", directory);
                        }
                        else
                        {
                            if (argv[2].IndexOfAny(invalidChars) == -1)
                            {
                                //IndexOfAny return -1 of no characters in array are found in target string
                                //valid command and filepath
                                //default to documents\yggdrasil statistics
                                //TODO: implement full path declaration, eg, C:\Bollocks\Project, probably involves changing ':' in invalidChars

                                directory = System.IO.Path.Combine(defaultDirectory, argv[2]);
                                directorySet = true;
                                Console.WriteLine("Directory set to {0}", directory);

                                FileCheck(directory);
                            }
                            else
                            {
                                //if invalid character(s) detected
                                Console.WriteLine("Invalid character detected.");
                                Console.WriteLine("The following characters are not allowed:");
                                foreach (char c in invalidChars)
                                {
                                    Console.Write("{0} ", c);
                                }
                                Console.WriteLine("Note: If declaring subfolders, \"\\\" must be used."); //sure is \ in here
                                Console.Write(Environment.NewLine);
                            }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid argument: default");
                        break;
                    }
            }
        }

        static void Write(string tweetLog)
        {
            directoryWithFile = System.IO.Path.Combine(directory, fileName);

            //create directories if they don't exist
            FileCheck(directory);

            //if csv doesn't already exist:
            if (!System.IO.Directory.Exists(directoryWithFile))
            {
                Console.WriteLine("Creating {0}", directory);
                //var csv = new StringBuilder();
                var headers = string.Format("{0},{1},{2}", "Date", "Time", "Tweet");
                System.IO.File.WriteAllText(directoryWithFile, headers);
            }

            ////var sb = new StringBuilder();
            //string newLine = string.Format("{0}{1},{2},{3}", Environment.NewLine, Timestamp.GetDateOnly(), Timestamp.GetTimeOnly(), "\"" + tweetLog + "\"");
            ////sb.Append(newLine);
            ////System.IO.File.AppendAllText(directoryWithFile, sb.ToString());

            //string csvContents = System.IO.File.ReadAllText(directoryWithFile);
            //csvContents += newLine;
            //System.IO.File.WriteAllText(directoryWithFile, csvContents);

        }

        static void FileCheck(string filePath)
        {
            //check filepath and create it if it doesn't exist

            string defaultPath = defaultDirectory;
            //removes defaultPath from filePath, creating a string with only subfolders
            string subPath = filePath.Replace(defaultPath, "");
            string tempPath;

            //remove first \ if present
            if (directory != defaultDirectory)
            {
                if (subPath.ToCharArray()[0] == '\\')
                {
                    subPath = subPath.Remove(0, 1);
                }
            }


            //get subfolder names
            string[] subfolders = subPath.Split('\\');

            //default filepath:
            //TODO: edit default filepath check if/when full path declaration is implemented
            if (!System.IO.Directory.Exists(defaultPath))
            {
                Console.WriteLine("Directory not found -- creating {0}", defaultPath);
                System.IO.Directory.CreateDirectory(defaultPath);
            }

            //create subfolders if they don't exist:
            tempPath = defaultPath;
            for (int i = 0; i < subfolders.Length; i++)
            {
                tempPath = System.IO.Path.Combine(tempPath, subfolders[i]);
                if (!System.IO.Directory.Exists(tempPath))
                {
                    System.IO.Directory.CreateDirectory(tempPath);
                }
            }
            //Console.WriteLine("filepath: {0}\ntemppath: {1}", filePath, tempPath);
            Console.WriteLine("Directory found/created: {0}", filePath);
        }
    }
}

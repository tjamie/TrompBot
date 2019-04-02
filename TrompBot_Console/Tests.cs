using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace TrompBot_Console
{
    class Tests
    {
        public static void ListTest()
        {
            int size = 3;

            List<string> testList = new List<string>();
            testList.Add("one");
            testList.Add("two");
            testList.Add("three");
            testList.Add("four");
            testList.Add("five");

            Console.WriteLine("Initial List:");
            foreach(string item in testList)
            {
                Console.Write(item + " ");
            }

            while (testList.Count < size)
            {
                testList.RemoveAt(0);
            }

            Console.WriteLine("Culled list:");
            foreach (string item in testList)
            {
                Console.Write(item + " ");
            }
        }

        public static void TweetMessage(string[] argv)
        {
            string text = String.Empty;
            string newText;
            string[] textArr;
            List<string> textList = new List<string>();

            #region debug: dump argv
            ////dump argv contents
            //Console.WriteLine("argv contents:");
            //for (int i = 0; i < argv.Length; i++)
            //{
            //    Console.Write("{0} {1} ", i, argv[i]);
            //}
            //Console.Write(Environment.NewLine);
            #endregion

            //splitting into array/rejoining in case this code needs to be reused later on
            for (int i = 1; i < argv.Length; i++)
            {
                textList.Add(argv[i]);
            }

            textArr = textList.ToArray();

            #region: dump text array
            //Array length and contents to console
            //Console.Write(Environment.NewLine);
            //Console.WriteLine("Array length: {0}", textArr.Length);
            //Console.WriteLine("Array formed:");
            //for (int i = 0; i < textArr.Length; i++)
            //{
            //    Console.Write("({0}) {1} ", i, textArr[i]);
            //}
            //Console.Write(Environment.NewLine);
            #endregion
            //Array to string
            //TODO: move tweet publishing and tweet modifying to separate methods later on
            newText = string.Join(" ", textArr);
            Console.WriteLine("Joined string: {0}", newText);

            //publish tweet
            Tweet.PublishTweet(newText);
        }
    }
}

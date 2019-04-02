using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;

namespace TrompBot_Console
{
    class Translate
    {
        //const string targetLanguage = "de";
        const string targetLanguage = "ru";

        public static void Start(string args)
        {
            string[] argv = args.Split(' ');

            //environment variable
            string creds = "C:\\Users\\Taylor\\Documents\\Code\\C#\\TrompBot_ClassicDesktopConsole\\TrompBot_Console\\TrompBot_Console\\GoogleCloudCredentials.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", creds);

            //string toTranslate = args.Substring(argv[0].Length + 1);
            string toTranslate = args;

            GetTranslation(toTranslate, targetLanguage);
        }

        static string GetTranslation(string input, string languageTo)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            TranslationClient client = TranslationClient.Create();
            var response = client.TranslateText(input, languageTo);
            string strText = response.TranslatedText;
            int firstIndex = strText.IndexOf(' ');


            string trimmedText = strText.Substring(firstIndex + 1);

            return trimmedText;
        }
    }
}

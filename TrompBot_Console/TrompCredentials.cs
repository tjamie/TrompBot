using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrompBot_Console
{
    static class TrompCredentials
    {
        public struct Credentials
        {
            public string consumerKey;
            public string consumerSecret;
            public string accessToken;
            public string accessTokenSecret;
        }

        public static Credentials GetCredentials()
        {
            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TrompBot");
            //Console.WriteLine("Path {0}", path);
            //Console.ReadLine();
            //var credJson = System.IO.Path.Combine(CSV.directory, "TrompCredentials.json");
            var credJson = System.IO.Path.Combine(path, "TrompCredentials.json");
            Console.WriteLine("Reading credentials file: {0}", credJson);
            JObject jObj = JObject.Parse(System.IO.File.ReadAllText(credJson));

            Credentials trompCreds = new Credentials();
            trompCreds.consumerKey = (string)jObj["consumerKey"];
            trompCreds.consumerSecret = (string)jObj["consumerSecret"];
            trompCreds.accessToken = (string)jObj["accessToken"];
            trompCreds.accessTokenSecret = (string)jObj["accessTokenSecret"];
            

            return trompCreds;
        }
    }
}

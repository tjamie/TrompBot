using System;
using System.Collections.Generic;
using System.Text;

namespace TrompBot_Console
{
    class Timestamp
    {
        public static string GetTime()
        {
            DateTime time = DateTime.Now;
            string format = "d MMM yyyy HH:mm:ss";
            return time.ToString(format);
        }

        public static string GetTimeOnly()
        {
            DateTime time = DateTime.Now;
            string format = "HH:mm:ss";
            return time.ToString(format);
        }

        public static string GetDateOnly()
        {
            DateTime time = DateTime.Now;
            string format = "d MMM yyyy";
            return time.ToString(format);
        }
    }
}

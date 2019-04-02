using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrompBot_Console
{
    class Number
    {
        public static int RNG(int min, int max)
        {
            System.Threading.Thread.Sleep(5); //to prevent multiple RNGs from producing the same value (based on system clock)
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }
    }
}

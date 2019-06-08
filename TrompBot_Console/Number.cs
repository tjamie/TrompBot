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

        public static int[] RandomBinary()
        {
            int seed = Number.RNG(536870912, 1073741823); // 2^29 to 2^30-1 and completely arbitrary

            char[] charArr = Convert.ToString(seed).ToCharArray();
            int[] bArr = new int[charArr.Length];

            for (int i = 0; i < charArr.Length; i++)
            {
                //bArr[i] = charArr[i] != '0';
                bArr[i] = Convert.ToInt32(charArr[i]);
            }

            return bArr;
        }
    }
}

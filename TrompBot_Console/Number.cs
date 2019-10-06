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
            System.Threading.Thread.Sleep(11); //to prevent multiple RNGs from producing the same value (based on system clock)
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        public static int[] RandomPreBinary()
        {
            //int seed = RNumber.RNG(536870912, 1073741823); // 2^29 to 2^30-1 and completely arbitrary
            int seed = RNG(1073741824, 2147483647); // 2^30 and 2^31 (unsigned)
            char[] charArr = Convert.ToString(seed).ToCharArray();
            int[] bArr = new int[charArr.Length];

            for (int i = 0; i < charArr.Length; i++)
            {
                //bArr[i] = charArr[i] != '0';

                bArr[i] = Convert.ToInt32(charArr[i]);
            }

            return bArr;
        }

        public static bool[] RandomBitArray(int iterations)
        {
            // Terrible way to do this but I can't be arsed to rewrite stuff for what's essentially an experiment
            int bitLength = 0;
            List<int[]> IntArrays = new List<int[]>();

            for (int i = 0; i < iterations; i++)
            {
                int[] tempArr = RandomPreBinary();
                IntArrays.Add(tempArr);
            }

            foreach (int[] arr in IntArrays)
            {
                bitLength += arr.Length;
            }

            bool[] bitArr = new bool[bitLength];

            // Go through each list item and go through each int of said item, keeping track of total bools accounted for
            int iBitArray = 0;
            foreach (int[] intArr in IntArrays)
            {
                for (int iItem = 0; iItem < intArr.Length; iItem++)
                {
                    bitArr[iBitArray] = EvenOddToBool(intArr[iItem]);
                    iBitArray++;
                }
            }

            return bitArr;
        }

        #region old
        //public static int[] RandomBinary()
        //{
        //    int seed = Number.RNG(536870912, 1073741823); // 2^29 to 2^30-1 and completely arbitrary

        //    char[] charArr = Convert.ToString(seed).ToCharArray();
        //    int[] bArr = new int[charArr.Length];

        //    for (int i = 0; i < charArr.Length; i++)
        //    {
        //        //bArr[i] = charArr[i] != '0';
        //        bArr[i] = Convert.ToInt32(charArr[i]);
        //    }

        //    return bArr;
        //}
        #endregion

        static bool EvenOddToBool(int i) => i % 2 == 0 ? true : false;
    }
}

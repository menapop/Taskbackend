using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Helpers
{
    public class RandomGenerator
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static  int RandomNumber(int min = 1, int max = int.MaxValue)
        {
            return random.Next(min, max);
        }
    }
}

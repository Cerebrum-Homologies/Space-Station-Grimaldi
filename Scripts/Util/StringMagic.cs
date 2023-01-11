using System;
using System.Text;
using System.Collections.Generic;
//using System.Collections.Specialized;

namespace SpaceStation.Util
{
    public class StringMagic
    {
        public static String RandomName(int minLen, int maxLen)
        {
            String res = "";
            var builder = new StringBuilder();

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = 'A';
            const int lettersOffset = 26; // A...Z or a..z
            Random _random = new Random();
            int size = _random.Next(minLen, maxLen);  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            res = builder.ToString();
            return res;
        }
    }
}
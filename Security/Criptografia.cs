using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Security
{
    public static class Criptografia
    {
        public static string GetMd5(this string text)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(text);
            var hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var t in hashBytes)
                sb.Append(t.ToString("X2"));
            return sb.ToString();
        }
        public static string GenerateString(int length = 10)
        {
            var array = new[]
                           {
                               "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r",
                               "s", "t", "u", "v", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                           };
            var symbols = new[] {".","*","+","-","@","#","!","&","%","=","_"};
            var alpha = new List<string>(array);
            alpha.AddRange(array.Select(t => t.ToUpper()));
            var random = new Random();
            var res = "";
            var sy = random.Next(0, length);
            for (var i = 0; i < length; i++)
            {
                if(i==sy)
                    res += symbols[random.Next(0, symbols.Length)];
                else
                    res += alpha[random.Next(0, alpha.Count)];
            }
            return res;
        }
    }
}

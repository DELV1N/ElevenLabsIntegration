using System;
using System.Security.Cryptography;
using System.Text;

namespace StringExtensions
{
    internal static class StringExtensions
    {
        public static Guid GenerateGuid(this string @string)
        {
            using var md5 = MD5.Create();
            return new Guid(md5.ComputeHash(Encoding.Default.GetBytes(@string)));
        }
    }
}
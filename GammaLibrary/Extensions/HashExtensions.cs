using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace GammaLibrary.Extensions
{
    public static class HashExtensions
    {
        public static byte[] MD5(this string input) => input.ToUTF8Bytes().MD5();

        public static byte[] MD5(this byte[] input) => System.Security.Cryptography.MD5.Create().Hash(input);

        public static byte[] SHA2(this string input) => input.ToUTF8Bytes().SHA2();

        public static byte[] SHA2(this byte[] input) => SHA256.Create().Hash(input);

        private static byte[] Hash(this HashAlgorithm hashAlgorithm, byte[] input)
        {
            using (hashAlgorithm) return hashAlgorithm.ComputeHash(input);
        }
    }
}
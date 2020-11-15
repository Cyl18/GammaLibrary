using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Threading;

namespace GammaLibrary.Extensions
{
    public static class HashExtensions
    {
        private static readonly ThreadLocal<MD5> _md5 = new(System.Security.Cryptography.MD5.Create);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Warning: MD5 is <c>UNSAFE</c>
        /// </remarks>
        public static byte[] MD5(this string input) => input.ToUTF8Bytes().MD5();

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// Warning: MD5 is <c>UNSAFE</c>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "<Pending>")]
        public static byte[] MD5(this byte[] input) => _md5.Value!.ComputeHash(input);
        
        private static readonly ThreadLocal<SHA256> _sha256 = new(System.Security.Cryptography.SHA256.Create);
        public static byte[] SHA256(this string input) => input.ToUTF8Bytes().SHA256();
        public static byte[] SHA256(this byte[] input) => _sha256.Value!.ComputeHash(input);


        private static readonly ThreadLocal<SHA1> _sha1 = new(System.Security.Cryptography.SHA1.Create);
        public static byte[] SHA1(this string input) => input.ToUTF8Bytes().SHA1();
        public static byte[] SHA1(this byte[] input) => _sha1.Value!.ComputeHash(input);


        private static readonly ThreadLocal<SHA512> _sha512 = new(System.Security.Cryptography.SHA512.Create);
        public static byte[] SHA512(this string input) => input.ToUTF8Bytes().SHA512();
        public static byte[] SHA512(this byte[] input) => _sha512.Value!.ComputeHash(input);

    }
}
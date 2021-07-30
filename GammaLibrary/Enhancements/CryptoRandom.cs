using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GammaLibrary.Enhancements
{
    public static class CryptoRandom
    {
        [ThreadStatic]
        static RNGCryptoServiceProvider? _threadLocalRandom;
        static RNGCryptoServiceProvider Rng =>
            _threadLocalRandom ??= new RNGCryptoServiceProvider();
        
        public static int NextPositiveInt()
            => Math.Abs(NextInt());

        public static int NextInt()
            => BitConverter.ToInt32(NextBytes(stackalloc byte[sizeof(int)]));
        public static uint NextUInt()
            => BitConverter.ToUInt32(NextBytes(stackalloc byte[sizeof(uint)]));

        public static long NextLong()
            => BitConverter.ToInt64(NextBytes(stackalloc byte[sizeof(long)]));
        public static ulong NextULong()
            => BitConverter.ToUInt64(NextBytes(stackalloc byte[sizeof(ulong)]));

        public static double NextDouble()
            => NextInt() * (0.5 / int.MaxValue) + 0.5;

        public static Span<byte> NextBytes(Span<byte> span)
        {
            Rng.GetBytes(span);
            return span;
        }

        public static byte[] NextBytes(byte[] array)
        {
            Rng.GetBytes(array);
            return array;
        }

        public static byte[] NextBytes(int length)
            => NextBytes(new byte[length]);

        public static void Dispose()
        {
            _threadLocalRandom?.Dispose();
            _threadLocalRandom = null!;
        }
    }
}

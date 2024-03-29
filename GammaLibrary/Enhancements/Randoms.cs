﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using GammaLibrary.Extensions;

namespace GammaLibrary.Enhancements
{
    // todo 今天不是很想写
    public static class Randoms
    {
        [ThreadStatic]
        static Random? _threadLocalRandom;
        static Random Rng => 
            _threadLocalRandom ??= new Random();

        public static int NextInt() => Rng.Next();
        public static int NextInt(Interval<int> interval)
        {
            var standardized = interval.Standardize();
            return Rng.Next(standardized.LeftEndpoint.Value, standardized.RightEndpoint.Value);
        }

        public static int NextDouble(Interval<double> interval)
        {
            throw new NotImplementedException();
        }
        //public static uint NextUInt() => (uint)NextInt() + (uint)NextInt();

        //public static bool NextBool() => NextInt() > int.MaxValue / 2;

        //public static ulong NextULong() => CombineInts(NextUInt(), NextUInt());
        public static int NextInt(int maxValue) => Rng.Next(maxValue);
        public static int NextInt(int minValue, int maxValue) => Rng.Next(minValue, maxValue);
        public static double NextDouble() => Rng.NextDouble();

        public static byte NextByte()
        {
            Span<byte> buffer = stackalloc byte[1];
            Rng.NextBytes(buffer);
            return buffer[0];
        }

        public static void NextBytes(Span<byte> buffer) => Rng.NextBytes(buffer);
        public static byte[] NextBytes(int bytesCount)
        {
            var buffer = new byte[bytesCount];
            Rng.NextBytes(buffer);
            return buffer;
        }

        public static void NextBytes(byte[] buffer) => Rng.NextBytes(buffer);

        static ulong CombineInts(uint a, uint b)
        {
            return (((ulong)a) << 32) | b;
        }

        public static int GetIntegerFromRange(Range range)
        {
            throw new NotImplementedException();
        }

        public static double GetDoubleFromRange(Range range)
        {
            throw new NotImplementedException();
        }
    }
}

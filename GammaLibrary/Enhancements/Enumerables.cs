using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Enhancements
{
    public static class Enumerables
    {
        // 我为啥要写这个？
        /*
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            for (var i = 0; i < count; i++)
            {
                yield return unchecked(start++);
            }
        }
        */

        public static void TwoDimensionalForeach(int startX, int endX, int startY, int endY, Action<int, int> worker)
        {
            for (var x = startX; x < endX; x++)
            {
                for (var y = startY; y < endY; y++)
                {
                    worker(x, y);
                }
            }
        }

        public static IEnumerable<T> OfElements<T>(params T[] elements)
        {
            return elements;
        }
    }
}

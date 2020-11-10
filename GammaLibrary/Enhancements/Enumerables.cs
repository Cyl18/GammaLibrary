using System;
using System.Collections.Generic;
using System.Linq;
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

        // From https://zhuanlan.zhihu.com/p/263408354
        static List<string> Recursion(IReadOnlyList<string[]> list, int start, List<string> result)
        {
            if (start >= list.Count)
                return result;

            if (result.Count == 0)
                result = list[start].ToList();
            else
                result = result.SelectMany(x => list[start].Select(y => x + y)).ToList();

            result = Recursion(list, start + 1, result);

            return result;
        }

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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GammaLibrary.Enhancements
{
    // todo 今天不是很想写
    public static class Randoms
    {
        private static ThreadLocal<Random> random = new ThreadLocal<Random>();



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

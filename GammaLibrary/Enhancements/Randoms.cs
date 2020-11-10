using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GammaLibrary.Enhancements
{
    // todo 今天不是很想写
    public class Randoms
    {
        private ThreadLocal<Random> random = new ThreadLocal<Random>();



        public int GetIntegerFromRange(Range range)
        {
            
            throw new NotImplementedException();
        }

        public double GetDoubleFromRange(Range range)
        {
            throw new NotImplementedException();
        }
    }
}

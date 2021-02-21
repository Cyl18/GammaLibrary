using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Common
{
    // todo
    internal ref struct GammaParser
    {
        Span<char> _buffer;

        public GammaParser(Span<char> buffer)
        {
            _buffer = buffer;
        }

        public char Peek()
        {
            return _buffer[0];
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Common
{
    public sealed class Container<T>
    {
        [NotNull]
        public T Value { get; set; }

        public Container(T value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string? ToString()
        {
            return Value.ToString();
        }
    }
}

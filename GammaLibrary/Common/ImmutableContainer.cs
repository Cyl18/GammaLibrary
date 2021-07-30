using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Common
{
    public sealed class ImmutableContainer<T> : IEquatable<ImmutableContainer<T>>, IComparable<ImmutableContainer<T>>, IComparable
    {
        [NotNull]
        public T Value { get; }

        public ImmutableContainer(T value)
        {
            // Contract.Requires(value != null, nameof(value) + " != null");
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int CompareTo(ImmutableContainer<T>? other)
        {
            if (Value is not IComparable<T> comparable) throw new InvalidOperationException("T must be IComparable.");
            return comparable.CompareTo(other!.Value);
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is ImmutableContainer<T> other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(ImmutableContainer<T>)}");
        }

        public static bool operator <(ImmutableContainer<T>? left, ImmutableContainer<T>? right)
        {
            return Comparer<ImmutableContainer<T>>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(ImmutableContainer<T>? left, ImmutableContainer<T>? right)
        {
            return Comparer<ImmutableContainer<T>>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(ImmutableContainer<T>? left, ImmutableContainer<T>? right)
        {
            return Comparer<ImmutableContainer<T>>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(ImmutableContainer<T>? left, ImmutableContainer<T>? right)
        {
            return Comparer<ImmutableContainer<T>>.Default.Compare(left, right) >= 0;
        }

        public bool Equals(ImmutableContainer<T>? other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object? obj) 
            => ReferenceEquals(this, obj) || obj is ImmutableContainer<T> other && Equals(other);

        public override int GetHashCode() 
            => EqualityComparer<T>.Default.GetHashCode(Value);

        public static bool operator ==(ImmutableContainer<T>? left, ImmutableContainer<T>? right) 
            => Equals(left, right);

        public static bool operator !=(ImmutableContainer<T>? left, ImmutableContainer<T>? right)
            => !Equals(left, right);
        
        public override string? ToString()
            => Value.ToString();
    }
}

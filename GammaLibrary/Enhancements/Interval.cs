using System;
using System.Numerics;
using System.Reflection;

namespace GammaLibrary.Enhancements
{
    public readonly struct Interval<T> where T : struct
    {
        public readonly Endpoint<T> LeftEndpoint;
        public readonly Endpoint<T> RightEndpoint;

        public Interval(Endpoint<T> leftEndpoint, Endpoint<T> rightEndpoint)
        {
            LeftEndpoint = leftEndpoint;
            RightEndpoint = rightEndpoint;
        }


        static readonly Lazy<T> MinValue = new(() =>
        {
            var type = typeof(T);
            return (T) (type.GetField("NegativeInfinity") ?? type.GetField("MinValue")!).GetValue(null)!;
        });

        static readonly Lazy<T> MaxValue = new(() =>
        {
            var type = typeof(T);
            return (T)(type.GetField("PositiveInfinity") ?? type.GetField("MaxValue")!).GetValue(null)!;
        });

        public static Interval<T> Of(string s)
        {
            const char splitter = ',';
            
            var inputSpan = s.AsSpan();
            var firstIndex = inputSpan.IndexOf(splitter);
            var lastIndex = inputSpan.LastIndexOf(splitter);
            if (firstIndex != lastIndex) throw new ArgumentException("Invalid string input. Multiple commas detected.");
            if (firstIndex == -1) throw new ArgumentException("Invalid string input. Comma not found.");
            var leftSpan = inputSpan[..firstIndex];
            var rightSpan = inputSpan[(firstIndex+1)..]; // 这里 ChangeType 竟然不支持 Span<char>.

            Endpoint<T> leftEndpoint, rightEndpoint;
            if (leftSpan.Length == 0)
            {
                leftEndpoint = new Endpoint<T>(MinValue.Value, EndpointType.Exclude);
            }
            else
            {
                var endpointType = GetEndpointType(leftSpan[0]);
                var number = (T)Convert.ChangeType(leftSpan[1..].ToString(), typeof(T), null);
                leftEndpoint = new Endpoint<T>(number, endpointType);
            }

            if (rightSpan.Length == 0)
            {
                rightEndpoint = new Endpoint<T>(MaxValue.Value, EndpointType.Exclude);
            }
            else
            {
                var endpointType = GetEndpointType(rightSpan[^1]);
                var number = (T)Convert.ChangeType(rightSpan[..^1].ToString(), typeof(T), null);
                rightEndpoint = new Endpoint<T>(number, endpointType);
            }

            return new Interval<T>(leftEndpoint, rightEndpoint);
            static EndpointType GetEndpointType(char c)
            {
                return c switch
                {
                    '(' or ')' => EndpointType.Exclude,
                    '[' or ']' => EndpointType.Include,
                    _ => throw new ArgumentException("Invalid string input. Endpoint is not valid.")
                };
            }
        }

        public static implicit operator Interval<T>(string s) => Of(s);
    }

    public static class IntervalFactory
    {

        public static Interval<int> OfInt(string s) => Interval<int>.Of(s);
        public static Interval<double> OfDouble(string s) => Interval<double>.Of(s);
    }

    public static class IntervalExtensions
    {
        public static Interval<int> Standardize(this Interval<int> interval)
        {
            var left = interval.LeftEndpoint;
            var right = interval.RightEndpoint;
            if (interval.LeftEndpoint.EndpointType == EndpointType.Exclude)
            {
                left = new Endpoint<int>(interval.LeftEndpoint.Value + 1, EndpointType.Include);
            }
            if (interval.RightEndpoint.EndpointType == EndpointType.Include)
            {
                right = new Endpoint<int>(interval.RightEndpoint.Value + 1, EndpointType.Exclude);
            }

            return new Interval<int>(left, right);
        }
    }

    public struct Endpoint<T> where T : struct
    {
        public T Value;
        public EndpointType EndpointType;

        public Endpoint(T value, EndpointType endpointType)
        {
            Value = value;
            EndpointType = endpointType;
        }
    }

    public enum EndpointType
    {
        Exclude,
        Include
    }
}
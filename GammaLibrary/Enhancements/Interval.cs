using System;
using System.Numerics;
using System.Reflection;

namespace GammaLibrary.Enhancements
{
    internal readonly struct Interval<T> where T : struct
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
            var rightSpan = inputSpan[firstIndex..]; // 这里 ChangeType 竟然不支持 Span<char>.

            Endpoint<T> leftEndpoint, rightEndpoint;
            if (leftSpan.Length == 0)
            {
                leftEndpoint = new Endpoint<T>(MinValue.Value, EndpointType.Open);
            }
            else
            {
                var endpointType = GetEndpointType(leftSpan[0]);
                var number = (T)Convert.ChangeType(leftSpan[1..].ToString(), typeof(T), null);
                leftEndpoint = new Endpoint<T>(number, endpointType);
            }

            if (rightSpan.Length == 0)
            {
                rightEndpoint = new Endpoint<T>(MaxValue.Value, EndpointType.Open);
            }
            else
            {
                var endpointType = GetEndpointType(rightSpan[^1]);
                var number = (T)Convert.ChangeType(leftSpan[..^1].ToString(), typeof(T), null);
                rightEndpoint = new Endpoint<T>(number, endpointType);
            }

            return new Interval<T>(leftEndpoint, rightEndpoint);
            static EndpointType GetEndpointType(char c)
            {
                return c switch
                {
                    '(' or ')' => EndpointType.Open,
                    '[' or ']' => EndpointType.Closed,
                    _ => throw new ArgumentException("Invalid string input. Endpoint is not valid.")
                };
            }
        }
        
    }

    internal struct Endpoint<T> where T : struct
    {
        public T Value;
        public EndpointType EndpointType;

        public Endpoint(T value, EndpointType endpointType)
        {
            Value = value;
            EndpointType = endpointType;
        }
    }

    internal enum EndpointType
    {
        Open,
        Closed
    }
}
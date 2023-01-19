#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Threading;
#endif

using System;

namespace imL
{
    public static partial class TestHelper
    {
        public static int MAX_INT(int _max)
        {
            _max++;
            Random _r = new Random();
            _max = _r.Next(_max);

            return _max;
        }
        public static long MAX_LONG(long _max)
        {
            _max++;
            byte[] _bytes = BitConverter.GetBytes(_max);
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(_bytes);
            Random _r = new Random();
            _r.NextBytes(_bytes);
            _max = BitConverter.ToInt64(_bytes, 0);

            return _max;
        }
    }
}
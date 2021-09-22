using System;
using System.Threading;

namespace imL.Utility
{
    public static class TestHelper
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

        public static void RandomSleep(int _max)
        {
            _max = MAX_INT(_max);

            if (_max > 0)
                Thread.Sleep(_max);
        }
        public static void RandomSleep(TimeSpan _time)
        {
            long _max = MAX_LONG(_time.Ticks);

            if (_max > 0)
                Thread.Sleep(new TimeSpan(_max));
        }
    }
}

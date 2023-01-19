#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Threading;
#endif

using System;

namespace imL
{
    public static partial class TestHelper
    {
#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
        public static void RandomThreadSleep(int _max)
        {
            _max = MAX_INT(_max);

            if (_max > 0)
                Thread.Sleep(_max);
        }
        public static void RandomThreadSleep(TimeSpan _time)
        {
            long _max = MAX_LONG(_time.Ticks);

            if (_max > 0)
                Thread.Sleep(new TimeSpan(_max));
        }
#endif

    }
}
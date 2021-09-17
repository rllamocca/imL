#if (NET35 || NET40) == false

using System;
using System.Threading;
using System.Threading.Tasks;

namespace imL.Utility
{
    public static class TestAsyncHelper
    {
        public async static Task RandomDelay(int _max, CancellationToken _token = default)
        {
            _max = TestHelper.MAX_INT(_max);

            if (_max > 0)
                await Task.Delay(_max, _token);
        }
        public async static Task RandomDelay(TimeSpan _time, CancellationToken _token = default)
        {
            long _max = TestHelper.MAX_LONG(_time.Ticks);

            if (_max > 0)
                await Task.Delay(new TimeSpan(_max), _token);
        }
    }
}

#endif
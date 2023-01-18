#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Threading;
using System.Threading.Tasks;

namespace imL
{
    public static class TestHelperAsync
    {
        public static async Task RandomDelayAsync(int _max, CancellationToken _token = default)
        {
            _max = TestHelper.MAX_INT(_max);

            if (_max > 0)
                await Task.Delay(_max, _token);
        }
        public static async Task RandomDelayAsync(TimeSpan _time, CancellationToken _token = default)
        {
            long _max = TestHelper.MAX_LONG(_time.Ticks);

            if (_max > 0)
                await Task.Delay(new TimeSpan(_max), _token);
        }
    }
}

#endif
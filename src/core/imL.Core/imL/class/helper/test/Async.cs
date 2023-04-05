#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Threading;
using System.Threading.Tasks;

namespace imL
{
    public static partial class TestHelper
    {
        public static async Task RandomTaskDelayAsync(int _max, CancellationToken _ct = default)
        {
            _max = MAX_INT(_max);

            if (_max > 0)
                await Task.Delay(_max, _ct);
        }
        public static async Task RandomTaskDelayAsync(TimeSpan _time, CancellationToken _ct = default)
        {
            long _max = MAX_LONG(_time.Ticks);

            if (_max > 0)
                await Task.Delay(new TimeSpan(_max), _ct);
        }
    }
}

#endif
using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace imL.Package.Hosting
{
    public partial class PeriodHostedService<GExecution>
    {
        public Task StartAsync(CancellationToken _ct)
        {
            if (_SETTING.Period == null || _SETTING.Period < 1)
                throw new ArgumentOutOfRangeException(nameof(_SETTING.Period));

            TimeSpan _period = TimeSpan.FromSeconds(_SETTING.Period.GetValueOrDefault());
            _LOGGER?.LogInformation("PeriodHostedService RUNNING: {_period}", _period);
            _TIMER = new Timer(DoWork, null, TimeSpan.Zero, _period);

            return _COMPLETEDTASK;
        }
        public Task StopAsync(CancellationToken _ct)
        {
            _TIMER?.Change(Timeout.Infinite, 0);
            _LOGGER?.LogInformation("PeriodHostedService is STOPPING.");

            return _COMPLETEDTASK;
        }
    }
}

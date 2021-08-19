using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace imL.Hosted.Frotcom.ToGPSChile
{
    public class PeriodHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<PeriodHostedService> _LOGGER;
        private ulong _EXECUTION_COUNT = 0;
        private Timer _TIMER;

        private async void DoWork(object _state)
        {
            ulong _count = Interlocked.Increment(ref this._EXECUTION_COUNT);

            this._LOGGER.LogInformation("WORKING DO. Count: {_count}", _count);
            await GPSChile.ExternalWork(this._LOGGER);
            //await Task.Delay(1000);
            this._LOGGER.LogInformation("STANDING WORK. Count: {_count}", _count);
        }


        public PeriodHostedService(ILogger<PeriodHostedService> _logger)
        {
            this._LOGGER = _logger;
        }
        public Task StartAsync(CancellationToken _ct)
        {
            TimeSpan _period = TimeSpan.FromSeconds(AppLocked.Setting.Endpoint.Period);
            this._LOGGER.LogInformation("TimedHostedService RUNNING. {_period}", Convert.ToString(_period));
            this._TIMER = new Timer(DoWork, null, TimeSpan.Zero, _period);

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken _ct)
        {
            this._LOGGER.LogInformation("TimedHostedService is STOPPING.");
            this._TIMER?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this._TIMER?.Dispose();
        }
    }
}

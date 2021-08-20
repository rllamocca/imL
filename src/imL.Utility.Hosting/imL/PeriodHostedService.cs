using System;
using System.Threading;
using System.Threading.Tasks;

using imL.Contract.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace imL.Utility.Hosting
{
    public class PeriodHostedService : IHostedService, IDisposable
    {
        private bool _DISPOSED = false;

        private readonly IHostPeriodWork _WORKER;
        private readonly IHostPeriodSetting _SETTING;
        private readonly ILogger<PeriodHostedService> _LOGGER;

        private long _EXECUTION_COUNT;
        private Timer _TIMER;

        private async void DoWork(object _state)
        {
            long _count = Interlocked.Increment(ref this._EXECUTION_COUNT);
            this._LOGGER?.LogDebug("WORKING DO: {_count}", _count);

            await this._WORKER?.DoWork(this._SETTING, this._LOGGER);

            this._LOGGER?.LogInformation("STANDING WORK: {_count}", _count);
        }


        public PeriodHostedService(
            IServiceProvider _services,
            IHostPeriodSetting _sett,
            ILogger<PeriodHostedService> _logger
            )
        {
            using (IServiceScope _scope = _services.CreateScope())
                this._WORKER = _scope.ServiceProvider.GetRequiredService<IHostPeriodWork>();

            this._SETTING = _sett;
            this._LOGGER = _logger;
        }
        public Task StartAsync(CancellationToken _ct)
        {
            TimeSpan _period = TimeSpan.FromSeconds(this._SETTING.Period);
            this._LOGGER?.LogInformation("PeriodHostedService RUNNING: {_period}", Convert.ToString(_period));
            this._TIMER = new Timer(DoWork, null, TimeSpan.Zero, _period);

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken _ct)
        {
            this._LOGGER?.LogInformation("PeriodHostedService is STOPPING.");
            this._TIMER?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        ~PeriodHostedService()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                this._TIMER?.Dispose();
            }

            this._DISPOSED = true;
        }
    }
}

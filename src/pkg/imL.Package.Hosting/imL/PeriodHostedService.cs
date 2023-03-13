using System;
using System.Threading;
using System.Threading.Tasks;

using imL;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace imL.Package.Hosting
{
    public class PeriodHostedService : PeriodHostedService<PeriodExecutionDefault>
    {
        public PeriodHostedService(
            IServiceProvider _services,
            IHostPeriodSetting _setting,
            IAppInfo _info,
            ILogger<PeriodHostedService<PeriodExecutionDefault>> _logger)
            : base(
                  _services,
                  _setting,
                  _info,
                  _logger)
        { }
    }

    public class PeriodHostedService<GExecution> : IHostedService, IDisposable
        where GExecution : IPeriodExecution, new()
    {
        bool _DISPOSED = false;
        readonly Task _COMPLETEDTASK = Task.CompletedTask;

        readonly IHostPeriodWorker _WORKER;
        readonly IHostPeriodSetting _SETTING;
        readonly IAppInfo _INFO;
        readonly ILogger<PeriodHostedService<GExecution>> _LOGGER;

        long _EXECUTION_COUNT;
        Timer _TIMER;

        async void DoWork(object _state)
        {
            long _count = Interlocked.Increment(ref _EXECUTION_COUNT);

            try
            {
                using (IPeriodExecution _using = new GExecution())
                {
                    CancellationTokenSource _cts = (_SETTING.Delay == 0) ? new CancellationTokenSource() : new CancellationTokenSource(TimeSpan.FromSeconds(_SETTING.Delay.GetValueOrDefault()));
                    _using.PopulateWithSomething(_count, _INFO, _cts.Token);
                    _using.AfterPopulate();
                    _LOGGER?.LogInformation("WORKING DO: {_p0} <<<<", _using.WorkingDoInfo());
                    await _WORKER?.DoWork(_using, _LOGGER);
                    _LOGGER?.LogInformation(">>>> STANDING WORK: {_p0}", _using.Guid);
                }
            }
            catch (Exception _ex)
            {
                _LOGGER?.LogCritical("EXCEPTION IN: {_count}", _count);
                _LOGGER?.LogCritical(_ex, "{p0}", _ex.Message);
            }
        }

        public PeriodHostedService(
            IServiceProvider _services,
            IHostPeriodSetting _setting,
            IAppInfo _info,
            ILogger<PeriodHostedService<GExecution>> _logger
            )
        {
            using (IServiceScope _scope = _services.CreateScope())
                _WORKER = _scope.ServiceProvider.GetRequiredService<IHostPeriodWorker>();

            _SETTING = _setting;
            _INFO = _info;
            _LOGGER = _logger;
        }
        public Task StartAsync(CancellationToken _ct)
        {
            TimeSpan _period = TimeSpan.FromSeconds(_SETTING.Period);
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

        //################################################################################
        ~PeriodHostedService()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (_DISPOSED)
                return;

            if (_managed)
            {
                _TIMER?.Dispose();
                _TIMER = null;
            }

            _DISPOSED = true;
        }
    }
}

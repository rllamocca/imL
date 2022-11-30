﻿using System;
using System.Threading;
using System.Threading.Tasks;

using imL.Contract;

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
            long _count = Interlocked.Increment(ref this._EXECUTION_COUNT);

            try
            {
                using (IPeriodExecution _using = new GExecution())
                {
                    CancellationTokenSource _cts = (this._SETTING.Delay == 0) ? new CancellationTokenSource() : new CancellationTokenSource(TimeSpan.FromSeconds(this._SETTING.Delay.GetValueOrDefault()));
                    _using.PopulateWithSomething(_count, this._INFO, _cts.Token);
                    _using.AfterPopulate();
                    this._LOGGER?.LogInformation("WORKING DO: {_p0} <<<<", _using.WorkingDoInfo());
                    await this._WORKER?.DoWork(_using, this._LOGGER);
                    this._LOGGER?.LogInformation(">>>> STANDING WORK: {_p0}", _using.Guid);
                }
            }
            catch (Exception _ex)
            {
                this._LOGGER?.LogCritical("EXCEPTION IN: {_count}", _count);
                this._LOGGER?.LogCritical(_ex, "{p0}", _ex.Message);
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
                this._WORKER = _scope.ServiceProvider.GetRequiredService<IHostPeriodWorker>();

            this._SETTING = _setting;
            this._INFO = _info;
            this._LOGGER = _logger;
        }
        public Task StartAsync(CancellationToken _ct)
        {
            TimeSpan _period = TimeSpan.FromSeconds(this._SETTING.Period);
            this._LOGGER?.LogInformation("PeriodHostedService RUNNING: {_period}", _period);
            this._TIMER = new Timer(DoWork, null, TimeSpan.Zero, _period);

            return this._COMPLETEDTASK;
        }
        public Task StopAsync(CancellationToken _ct)
        {
            this._TIMER?.Change(Timeout.Infinite, 0);
            this._LOGGER?.LogInformation("PeriodHostedService is STOPPING.");

            return this._COMPLETEDTASK;
        }

        //################################################################################
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
                this._TIMER = null;
            }

            this._DISPOSED = true;
        }
    }
}

//using System;
//using System.Threading;
//using System.Threading.Tasks;
////using System.Timers;

//using imL.Contract;
//using imL.Contract.Hosting;
//using imL.Utility.Hosting.FulFill;

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;

//namespace imL.Utility.Hosting
//{
//    public sealed class PeriodHostedService2<GExecution> : IHostedService, IAsyncDisposable
//        where GExecution : IExecution, new()
//    {
//        private bool _DISPOSED = false;
//        private readonly Task _COMPLETEDTASK = Task.CompletedTask;

//        private readonly IHostPeriodWorker _WORKER;
//        private readonly IHostPeriodSetting _SETTING;
//        private readonly IAppInfo _INFO;
//        private readonly ILogger<PeriodHostedService2<GExecution>> _LOGGER;

//        private long _EXECUTION_COUNT;
//        private Timer _TIMER;

//        private async void DoWork(object _state)
//        {
//            long _count = Interlocked.Increment(ref this._EXECUTION_COUNT);

//            try
//            {
//                using (IExecution _using = new GExecution())
//                {
//                    _using.PopulateWithSomething(_count, this._INFO, new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
//                    _using.AfterPopulate();
//                    this._LOGGER?.LogInformation("WORKING DO: {_p0} <<<<", _using.WorkingDoInfo());
//                    await this._WORKER?.DoWork(_using, this._LOGGER);
//                    this._LOGGER?.LogInformation(">>>> STANDING WORK: {_p0}", _using.Guid);
//                }
//            }
//            catch (Exception _ex)
//            {
//                this._LOGGER?.LogCritical("EXCEPTION IN: {_count}", _count);
//                this._LOGGER?.LogCritical(_ex, _ex.Message);
//            }
//        }

//        public PeriodHostedService2(
//            IServiceProvider _services,
//            IHostPeriodSetting _setting,
//            IAppInfo _info,
//            ILogger<PeriodHostedService2<GExecution>> _logger
//            )
//        {
//            using (IServiceScope _scope = _services.CreateScope())
//                this._WORKER = _scope.ServiceProvider.GetRequiredService<IHostPeriodWorker>();

//            this._SETTING = _setting;
//            this._INFO = _info;
//            this._LOGGER = _logger;
//        }
//        public Task StartAsync(CancellationToken _ct)
//        {
//            TimeSpan _period = TimeSpan.FromSeconds(this._SETTING.Period);
//            this._LOGGER?.LogInformation("PeriodHostedService RUNNING: {_period}", _period);
//            //this._LOGGER?.LogInformation("TokenStartAsync: {0}", _ct.IsCancellationRequested);
//            this._TIMER = new Timer(DoWork, null, TimeSpan.Zero, _period);

//            return this._COMPLETEDTASK;
//        }
//        public Task StopAsync(CancellationToken _ct)
//        {
//            //this._LOGGER?.LogInformation("TokenStopAsync: {0}", _ct.IsCancellationRequested);
//            this._TIMER?.Change(Timeout.Infinite, 0);
//            this._LOGGER?.LogInformation("PeriodHostedService is STOPPING.");

//            return this._COMPLETEDTASK;
//        }



//        //################################################################################
//        public async ValueTask DisposeAsync()
//        {
//            if (this._TIMER is IAsyncDisposable _timer)
//            {
//                await _timer.DisposeAsync();
//            }

//            this._TIMER = null;
//        }
//        //~PeriodHostedService2()
//        //{
//        //    this.Dispose(false);
//        //}
//        //public void Dispose()
//        //{
//        //    this.Dispose(true);
//        //    GC.SuppressFinalize(this);
//        //}
//        //protected virtual void Dispose(bool _managed)
//        //{
//        //    if (this._DISPOSED)
//        //        return;

//        //    if (_managed)
//        //    {
//        //        this._TIMER?.Dispose();
//        //    }

//        //    this._DISPOSED = true;
//        //}
//    }
//}

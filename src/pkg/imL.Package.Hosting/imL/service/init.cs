using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace imL.Package.Hosting
{
    public partial class PeriodHostedService<GExecution> :
        IHostedService, IDisposable
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
    }

    public partial class PeriodHostedService : PeriodHostedService<PeriodExecutionDefault>
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
}

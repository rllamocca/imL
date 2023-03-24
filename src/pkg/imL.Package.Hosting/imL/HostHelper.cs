using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace imL.Package.Hosting
{
    public class HostHelper
    {
        public static IHostBuilder CreatePeriodHostBuilder<GExecution, GWorker>
            (IAppInfo _info, IHostPeriodSetting _setting)
            where GExecution : class, IPeriodExecution, new()
            where GWorker : class, IHostPeriodWorker
        {
            return Host.CreateDefaultBuilder(_info.args)
                .ConfigureServices((_hc, _asc) =>
                {
                    _asc.AddHostedService<PeriodHostedService<GExecution>>();

                    _asc.AddSingleton(_s => _info)
                    .AddSingleton(_s => _setting);

                    _asc.AddScoped<IHostPeriodWorker, GWorker>();
                });
        }
        public static IHostBuilder CreatePeriodHostBuilder<GWorker>
            (IAppInfo _info, IHostPeriodSetting _setting)
            where GWorker : class, IHostPeriodWorker
        {
            return CreatePeriodHostBuilder<PeriodExecutionDefault, GWorker>(_info, _setting);
        }
    }
}

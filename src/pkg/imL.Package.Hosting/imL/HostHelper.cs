using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace imL.Package.Hosting
{
    public class HostHelper
    {
        public static IHostBuilder CreateHostBuilder<GWorker>(IAppInfo _info, IHostPeriodSetting _setting)
            where GWorker : class, IHostPeriodWorker
        {
            return Host.CreateDefaultBuilder(_info.args)
                .ConfigureServices((_hc, _sc) =>
                {
                    _sc.AddHostedService<PeriodHostedService>();

                    _sc.AddSingleton(_s => _info)
                    .AddSingleton(_s => _setting);

                    _sc.AddScoped<IHostPeriodWorker, GWorker>();
                })
                .UseConsoleLifetime();
        }
    }
}

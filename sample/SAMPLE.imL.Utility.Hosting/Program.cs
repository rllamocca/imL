using System.Threading.Tasks;

using imL.Contract;
using imL.Package.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace SAMPLE.imL.Utility.Hosting
{
    public class Program
    {
        async static Task Main()
        {
            string[] _args = new string[] { "Richie", "Tepes" };
            MyLocked.Load(new AppInfoDefault(_args));
            await CreateHostBuilder(_args).RunConsoleAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] _args)
        {
            return Host.CreateDefaultBuilder(_args)
                .ConfigureServices((_hc, _sc) =>
                {
                    _sc.AddHostedService<PeriodHostedService>();
                    _sc.AddScoped<IHostPeriodWorker, MyWorker>();
                    _sc.AddSingleton<IHostPeriodSetting, HostedSetting>(_s => MyLocked.Setting.Hosted);
                })
                .ConfigureLogging(_lg =>
                {
                    _lg.ClearProviders();
#if DEBUG
                    _lg.SetMinimumLevel(LogLevel.Trace);
#else
                    _lg.SetMinimumLevel(LogLevel.Information);
#endif
                    _lg.AddNLog();

                    NLog.LogManager.Configuration.Variables["_BASEDIR_"] = MyLocked.App.PathLog;
                });
        }
    }
}

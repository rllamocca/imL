using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using imL;
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

            IAppInfo _info = new AppInfoDefault(_args);
            MySetting _setting = JsonSerializer.Deserialize<MySetting>(File.ReadAllText(Path.Combine(_info.Base, "settings.json")));

            await CreateHostBuilder(_args, _setting, _info)
                .RunConsoleAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] _args, IHostPeriodSetting _hosted, IAppInfo _info)
        {
            return Host.CreateDefaultBuilder(_args)
                .ConfigureServices((_hc, _sc) =>
                {
                    _sc.AddHostedService<PeriodHostedService>();
                    _sc.AddSingleton(_s => _hosted);
                    _sc.AddSingleton(_s => _info);
                    _sc.AddScoped<IHostPeriodWorker, MyWorker>();
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
                });
        }
    }
}

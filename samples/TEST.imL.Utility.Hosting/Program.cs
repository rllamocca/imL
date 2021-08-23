using System.Threading.Tasks;

using imL.Contract.Hosting;
using imL.Utility.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace TEST.imL.Utility.Hosting
{
    class Program
    {
        async static Task Main(string[] _args)
        {
            _args = new string[] { "Richie", "Tepes" };

            AppLocked.Init(_args);
            await CreateHostBuilder(_args).RunConsoleAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] _args)
        {
            return Host.CreateDefaultBuilder(_args)
                .UseConsoleLifetime()
                .ConfigureLogging(_lg =>
                {
                    _lg.ClearProviders();
#if DEBUG
                    _lg.SetMinimumLevel(LogLevel.Trace);
#else
                    _lg.SetMinimumLevel(LogLevel.Information);
#endif
                    _lg.AddNLog();
                })
                .ConfigureServices(_ss =>
                {
                    _ss.AddSingleton<IHostPeriodSetting, HostedSetting>(_s => AppLocked.Setting.Hosted);
                    _ss.AddScoped<IHostPeriodWork, TestWork>();
                    _ss.AddHostedService<PeriodHostedService>();
                });
        }
    }
}

/*
var config = new ConfigurationBuilder()
.SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.Build();
 */
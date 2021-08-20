using System;
using System.Text;
using System.Threading.Tasks;

using imL.Contract.Hosting;
using imL.Utility.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace imL.Hosted.Frotcom.ToGPSChile
{
    public class Program
    {
        public async static Task Main(string[] _args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            /*
var config = new ConfigurationBuilder()
.SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.Build();
             */
            AppLocked.Init(_args);

            await CreateHostBuilder(_args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] _args)
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
                    _ss.AddSingleton<IHostSetting, HostedSetting>(_s => AppLocked.Setting.Hosted);
                    _ss.AddScoped<IPeriodWork, TestWork>();
                    _ss.AddHostedService<PeriodHostedService>();
                });
        }
    }
}
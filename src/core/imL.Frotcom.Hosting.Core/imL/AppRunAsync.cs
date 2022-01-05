using System;
using System.Threading.Tasks;

using imL.Contract;
using imL.Fulfill;
using imL.Utility.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Hosting;

namespace imL.Frotcom.Hosting.Core
{
    public class AppRunAsync
    {
        private static readonly object _LOCKED = new object();
        private static Microsoft.Extensions.Logging.ILogger _LOGGER;

        public static Microsoft.Extensions.Logging.ILogger Logger { get { lock (AppRunAsync._LOCKED) { return AppRunAsync._LOGGER; } } }

        public static async Task ConsoleAsync<GWorker>
            (IHostPeriodSetting _setting, IAppInfo _info)
            where GWorker : class, IHostPeriodWorker
        {
            await ConsoleAsync<PeriodExecutionDefault, GWorker>(_setting, _info);
        }
        public static async Task ConsoleAsync<GExecution, GWorker>
            (IHostPeriodSetting _setting, IAppInfo _info)
            where GExecution : class, IPeriodExecution, new()
            where GWorker : class, IHostPeriodWorker
        {
            try
            {
                IHostBuilder _build = Host.CreateDefaultBuilder(_info.Args)
                    .ConfigureServices(_ac =>
                    {
                        _ac.AddHostedService<PeriodHostedService<GExecution>>();
                        _ac.AddScoped<IHostPeriodWorker, GWorker>();
                        _ac.AddSingleton(_s => _setting);
                        _ac.AddSingleton(_s => _info);
                    })
                    .UseConsoleLifetime()
                    .UseSimpleLogging(
#if DEBUG
                        Enumeration.Logging.EConsoleOutput.Simple
#endif
                        );

                if (BAppLocked.App.InContainer == false)
                {
                    LogManager.Configuration.Variables["mybasedir"] = _info.PathLog;
                    _build.UseNLog();
                }

                IHost _host = _build.Build();
                AppRunAsync._LOGGER = _host.Services.GetRequiredService<ILogger<AppRunAsync>>();
                AppRunAsync._LOGGER?.LogInformation("Host created.");

                await _host.RunAsync();
            }
            catch (Exception _ex)
            {
                string _msg = "Stopped program because of exception";

                if (AppRunAsync._LOGGER == null)
                {
                    Console.WriteLine(_msg);
                    Console.WriteLine(_ex);
                }
                else
                    AppRunAsync._LOGGER?.LogCritical(_ex, "{p0}", _msg);

                throw;
            }
            finally
            {
                if (BAppLocked.App.InContainer == false)
                    LogManager.Shutdown();
            }
        }
    }
}

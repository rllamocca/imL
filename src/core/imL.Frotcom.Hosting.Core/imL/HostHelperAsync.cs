using System;
using System.Threading.Tasks;

using imL.Contract;
using imL.Enumeration.Logging;
using imL.Utility.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Hosting;

using _NSP_LOGGING = Microsoft.Extensions.Logging;

namespace imL.Frotcom.Hosting.Core
{
    public class HostHelperAsync
    {
        private static readonly object _LOCKED = new object();
        private static _NSP_LOGGING.ILogger _LOGGER;

        public static _NSP_LOGGING.ILogger Logger { get { lock (HostHelperAsync._LOCKED) { return HostHelperAsync._LOGGER; } } }

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
                        EConsoleOutput.Simple
#endif
                        );

                if (LockedHost.App.InContainer == false)
                {
                    LogManager.AutoShutdown = true;
                    LogManager.Configuration.Variables["_BASEDIR_"] = _info.PathLog;
                    _build.UseNLog();
                }

                IHost _host = _build.Build();
                HostHelperAsync._LOGGER = _host.Services.GetRequiredService<ILogger<HostHelperAsync>>();
                HostHelperAsync._LOGGER?.LogInformation("Host created.");

                await _host.RunAsync();
            }
            catch (Exception _ex)
            {
                string _msg = "Stopped program because of exception";

                if (HostHelperAsync._LOGGER == null)
                {
                    Console.WriteLine(_msg);
                    Console.WriteLine(_ex);
                }
                else
                    HostHelperAsync._LOGGER?.LogCritical(_ex, "{p0}", _msg);

                throw;
            }
            finally
            {
                if (LockedHost.App.InContainer == false)
                    LogManager.Shutdown();
            }
        }
    }
}

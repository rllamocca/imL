using System;
using System.Threading.Tasks;

using imL.Contract;
using imL.Enumeration.Logging;
using imL.Package.Hosting;
using imL.Package.Logging;
using imL.Utility;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Hosting;

namespace imL.Frotcom.Hosting.Core
{
    public class HostHelperAsync
    {
        public static async Task ConsoleAsync<GWorker>
            (IHostPeriodSetting _setting, IAppInfo _info, EConsoleFormatter _formatter = EConsoleFormatter.Simple)
            where GWorker : class, IHostPeriodWorker
        {
            await ConsoleAsync<PeriodExecutionDefault, GWorker>(_setting, _info, _formatter);
        }
        public static async Task ConsoleAsync<GExecution, GWorker>
            (IHostPeriodSetting _setting, IAppInfo _info, EConsoleFormatter _formatter = EConsoleFormatter.Simple)
            where GExecution : class, IPeriodExecution, new()
            where GWorker : class, IHostPeriodWorker
        {
            Microsoft.Extensions.Logging.ILogger _logger = null;

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
                    .UseSimpleLogging(_formatter);

                if (_info.InContainer == false)
                {
                    LogManager.AutoShutdown = true;
                    LogManager.Configuration.Variables["_BASEDIR_"] = _info.PathLog;
                    _build.UseNLog();
                }

                IHost _host = _build.Build();
                _logger = _host.Services.GetRequiredService<ILogger<HostHelperAsync>>();
                _logger?.LogInformation("Host created.");

                await _host.RunAsync();
            }
            catch (Exception _ex)
            {
                string _msg = "Stopped app because of exception.";

                if (_logger == null)
                {
                    Console.WriteLine(_msg);
                    ConsoleHelper.InnerException(_ex);
                }
                else
                {
                    _logger?.LogCritical(_msg);
                    _logger?.InnerLogCritical(_ex);
                }

                throw;
            }
            finally
            {
                if (_info.InContainer == false)
                    LogManager.Shutdown();
            }
        }
    }
}

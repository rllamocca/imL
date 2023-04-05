using System;
using System.Threading.Tasks;

using imL.Package.Hosting;
using imL.Package.Logging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Hosting;

namespace imL.Frotcom.Hosting.Core
{
    public class HostHelperAsync
    {
        public static async Task ConsoleAsync<GExecution, GWorker>
            (IAppInfo _info, IHostPeriodSetting _setting)
            where GExecution : class, IPeriodExecution, new()
            where GWorker : class, IHostPeriodWorker
        {
            ILogger _logger = null;

            try
            {
                IHostBuilder _builder = HostHelper.CreatePeriodHostBuilder<GExecution, GWorker>(_info, _setting);

                if (_info.InContainer != true)
                {
                    _builder.UseNLog();

                    NLog.LogManager.AutoShutdown = true;
                }

                IHost _host = _builder.Build();
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
                    ConsoleHelper.WriteInnerException(_ex);
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
                if (_info.InContainer != true)
                    NLog.LogManager.Shutdown();
            }
        }
        public static async Task ConsoleAsync<GWorker>
            (IAppInfo _info, IHostPeriodSetting _setting)
            where GWorker : class, IHostPeriodWorker
        {
            await ConsoleAsync<PeriodExecutionDefault, GWorker>(_info, _setting);
        }
    }
}

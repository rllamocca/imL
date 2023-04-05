using System.Threading.Tasks;

using imL.Package.Hosting;

using Microsoft.Extensions.Logging;

namespace SAMPLE.imL.Utility.Hosting
{
    internal class MyWorker : IHostPeriodWorker
    {
        public async Task DoWork(IPeriodExecution _execution, IHostPeriodSetting _settings, ILogger _logger)
        {
            _logger?.LogInformation("Worker: {0} {1} {2}", _execution.Count, _execution.App.args[0], _execution);
            await Task.Delay(1000);
        }
    }
}

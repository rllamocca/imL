using imL.Utility.Hosting;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace SAMPLE.imL.Utility.Hosting
{
    internal class MyWorker : IHostPeriodWorker
    {
        public async Task DoWork(IPeriodExecution _execution, ILogger _logger)
        {
            _logger?.LogInformation("Worker: {0} {1}", _execution, _execution.App.Args[0]);
            await Task.Delay(1000);
        }
    }
}

using System.Threading.Tasks;

using imL.Contract;

using Microsoft.Extensions.Logging;

namespace SAMPLE.imL.Utility.Hosting
{
    internal class Worker : IHostPeriodWorker
    {
        public async Task DoWork(IPeriodExecution _execution, ILogger _logger)
        {
            _logger?.LogInformation("Worker: {0} {1}", _execution, _execution.App.Args[0]);
            await Task.Delay(1000);
        }
    }
}

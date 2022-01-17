using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace imL.Package.Hosting
{
    public interface IHostPeriodWorker
    {
        Task DoWork(IPeriodExecution _execution, ILogger _logger);
    }
}

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace imL.Contract
{
    public interface IHostPeriodWorker
    {
        Task DoWork(IPeriodExecution _execution, ILogger _logger);
    }
}

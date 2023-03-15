using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace imL.Package.Hosting
{
    public interface IHostPeriodWorker
    {
        Task DoWork(IPeriodExecution _execution, IHostPeriodSetting _settings, ILogger _logger);
    }
}

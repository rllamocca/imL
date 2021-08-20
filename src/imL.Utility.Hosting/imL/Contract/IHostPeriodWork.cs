using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace imL.Contract.Hosting
{
    public interface IHostPeriodWork
    {
        Task DoWork(IHostPeriodSetting _setting, ILogger _logger);
    }
}

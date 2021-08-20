using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace imL.Utility.Contract.Hosting
{
    public interface IPeriodWork
    {
        Task DoWork(ILogger _logger);
    }
}

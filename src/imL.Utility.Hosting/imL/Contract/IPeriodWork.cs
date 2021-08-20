using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace imL.Contract.Hosting
{
    public interface IPeriodWork
    {
        Task DoWork(ILogger _logger);
    }
}

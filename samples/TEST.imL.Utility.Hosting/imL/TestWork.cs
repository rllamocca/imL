using System.Threading.Tasks;

using imL.Contract.Hosting;

using Microsoft.Extensions.Logging;

namespace TEST.imL.Utility.Hosting
{
    public class TestWork : IPeriodWork
    {
        public async Task DoWork(ILogger _logger)
        {
            _logger?.LogInformation("TestWork");
            await Task.Delay(1000);
        }
    }
}

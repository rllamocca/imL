using System.Threading.Tasks;

using imL.Utility.Contract.Hosting;

using Microsoft.Extensions.Logging;

namespace imL.Hosted.Frotcom.ToGPSChile
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

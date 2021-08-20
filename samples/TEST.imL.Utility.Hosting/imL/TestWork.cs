using System.Threading.Tasks;

using imL.Contract.Hosting;

using Microsoft.Extensions.Logging;

namespace TEST.imL.Utility.Hosting
{
    public class TestWork : IHostPeriodWork
    {
        public async Task DoWork(IHostPeriodSetting _setting, ILogger _logger)
        {
            _logger?.LogInformation("TestWork {0}", _setting.Args[0]);
            await Task.Delay(1000);
        }
    }
}

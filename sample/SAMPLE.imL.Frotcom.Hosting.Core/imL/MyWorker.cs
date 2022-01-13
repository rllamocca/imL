using imL.Frotcom.Hosting.Core;
using imL.Rest.Frotcom;
using imL.Utility.Hosting;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MyWorker : IHostPeriodWorker
    {
        public async Task DoWork(IPeriodExecution _execution, ILogger _logger)
        {
            _logger?.LogInformation("Token: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);

            FrotcomClient _frotcom = new(MyLocked.Http, MyLocked.Setting.Frotcom);
            await CoreHelperAsync.FillTokenCachedAsync(_frotcom);
            Dough[] _doughs = await CoreHelperAsync.PreparedAsync(_frotcom, _logger);

            await Task.Delay(5000);
            _execution.Token.ThrowIfCancellationRequested();

            _logger?.LogInformation("Token Delay: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);
        }
    }
}

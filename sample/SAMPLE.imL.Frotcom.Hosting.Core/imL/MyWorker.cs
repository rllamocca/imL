using imL.Contract;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MyWorker : IHostPeriodWorker
    {
        public async Task DoWork(IPeriodExecution _execution, ILogger _logger)
        {
            _logger?.LogInformation("Token: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);

            await Task.Delay(5000);
            _execution.Token.ThrowIfCancellationRequested();

            _logger?.LogInformation("Token Delay: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);
        }
    }
}

using imL.Frotcom.Hosting.Core;
using imL.Package.Hosting;
using imL.Rest.Frotcom;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MyWorker : IHostPeriodWorker
    {
        public async Task DoWork(IPeriodExecution _execution, IHostPeriodSetting _settings, ILogger _logger)
        //, IHostSetting _settings
        {
            _logger?.LogInformation("Token: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);

            MySetting _setting = (MySetting)_settings;
            FrotcomClient _frotcom = new(_setting.Frotcom);
            _frotcom.Authorize = await CoreHelperAsync.FillTokenCachedAsync(_frotcom);
            IEnumerable<Dough> _doughs = await CoreHelperAsync.PreparedAsync(_frotcom, _logger);
            _doughs = await _frotcom.ToPrepareAsync();

            await Task.Delay(5000);
            _execution.Token.ThrowIfCancellationRequested();

            _logger?.LogInformation("Token: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);
        }
    }
}

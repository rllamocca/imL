using imL.Frotcom.Hosting.Core;
using imL.Package.Hosting;
using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MyWorker : IHostPeriodWorker
    {
        public async Task DoWork(IPeriodExecution _execution, IHostPeriodSetting _settings, ILogger _logger)
        {
            _logger?.LogInformation("Token: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);

            MySetting _setting = (MySetting)_settings;
            FrotcomClient _frotcom = FrotcomClient.GetSingleton(_setting.Frotcom);

            _logger?.LogInformation("HI 5");
            _logger?.LogInformation("{p0}", FrotcomClient.GetBaseAddress());

            _frotcom.Authorize = await CoreHelperAsync.FillTokenCachedAsync(_frotcom);

            IEnumerable<Vehicle200> _vehicles = await _frotcom.GetVehiclesAsync();
            _logger?.LogInformation("Count: {p0}", _vehicles.Count());
            _logger?.LogInformation("First: {p0}", _vehicles.First().licensePlate);
            _logger?.LogInformation("Last: {p0}", _vehicles.Last().licensePlate);

            //IEnumerable<Dough> _doughs = await CoreHelperAsync.PreparedAsync(_frotcom, _logger);
            //_doughs = await _frotcom.ToPrepareAsync();

            await Task.Delay(2000);
            _execution.Token.ThrowIfCancellationRequested();

            _logger?.LogInformation("Token: {p0} {p1}", _execution, _execution.Token.IsCancellationRequested);
        }
    }
}

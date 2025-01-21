using System;
using System.Threading;

using Microsoft.Extensions.Logging;

namespace imL.Package.Hosting
{
    public partial class PeriodHostedService<GExecution>
    {
        async void DoWork(object _state)
        {
            long _count = Interlocked.Increment(ref _EXECUTION_COUNT);

            try
            {
                using (IPeriodExecution _using = new GExecution())
                {
                    CancellationTokenSource _cts = (_SETTING.TimeOut == null) ? new CancellationTokenSource() : new CancellationTokenSource(TimeSpan.FromSeconds(_SETTING.TimeOut.GetValueOrDefault()));
                    _using.PopulateWithSomething(_count, _INFO, _cts.Token);
                    _using.AfterPopulate();
                    _LOGGER?.LogInformation("WORKING DO: {_p0} <<<<", _using.WorkingDoInfo());
                    await _WORKER?.DoWork(_using, _SETTING, _LOGGER);
                    _LOGGER?.LogInformation(">>>> STANDING WORK: {_p0}", _using.Guid);
                }
            }
            catch (Exception _ex)
            {
                _LOGGER?.LogCritical("EXCEPTION IN: {_count}", _count);
                _LOGGER?.LogCritical(_ex, "{p0}", _ex.Message);
            }
        }
    }
}

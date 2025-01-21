using System;

namespace imL.Package.Hosting
{
    public partial class PeriodHostedService<GExecution>
    {
        ~PeriodHostedService()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (_DISPOSED)
                return;

            if (_managed)
            {
                _TIMER?.Dispose();
                _TIMER = null;
            }

            _DISPOSED = true;
        }
    }
}

using imL;
using imL.Package.Hosting;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MyExecution : IPeriodExecution
    {
        private bool _DISPOSED;

        private long _COUNT;
        private IAppInfo? _APP;
        private CancellationToken _TOKEN;

        private DateTime _START;
        private string? _GUID;

        public long Count { get { return _COUNT; } }
        public IAppInfo? App { get { return _APP; } }
        public CancellationToken Token { get { return _TOKEN; } }

        public DateTime Start { get { return _START; } }
        public string? Guid { get { return _GUID; } }

        public void PopulateWithSomething(long _count, IAppInfo _app, CancellationToken _token = default)
        {
            _COUNT = _count;
            _APP = _app;
            _TOKEN = _token;

            _START = DateTime.Now;
            _GUID = string.Format("[{0}]GUID", _COUNT);
        }
        public void AfterPopulate()
        {
            //throw new NotImplementedException();
        }

        public string WorkingDoInfo()
        {
            return string.Format("MyExecution {0} | {1}|", _START.ToLocalTime(), _GUID);
        }

        public override string ToString()
        {
            return string.Format("{0} |", _GUID);
        }

        //################################################################################
        ~MyExecution()
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

            }

            _DISPOSED = true;
        }
    }
}

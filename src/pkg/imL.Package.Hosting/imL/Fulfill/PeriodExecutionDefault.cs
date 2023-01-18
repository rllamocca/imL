using System;
using System.Threading;

using imL.Contract;

namespace imL.Package.Hosting
{
    public class PeriodExecutionDefault : IPeriodExecution
    {
        bool _DISPOSED;

        long _COUNT;
        IAppInfo _APP;
        CancellationToken _TOKEN;

        DateTime _START;
        string _GUID;

        public long Count { get { return _COUNT; } }
        public IAppInfo App { get { return _APP; } }
        public CancellationToken Token { get { return _TOKEN; } }

        public DateTime Start { get { return _START; } }
        public string Guid { get { return _GUID; } }

        public void PopulateWithSomething(long _count, IAppInfo _app, CancellationToken _token = default)
        {
            _START = DateTime.Now;

            _COUNT = _count;
            _APP = _app;
            _TOKEN = _token;

            _GUID = Convert.ToString(System.Guid.NewGuid());
        }
        public void AfterPopulate()
        {
            //throw new NotImplementedException();
        }

        public string WorkingDoInfo()
        {
            return string.Format("{0} | {1}| {2}|", _START.ToLocalTime(), _GUID, _COUNT);
        }

        public override string ToString()
        {
            return string.Format("[{0}]{1}|", _COUNT, _GUID);
        }

        //################################################################################
        ~PeriodExecutionDefault()
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
                //
            }

            _DISPOSED = true;
        }
    }
}

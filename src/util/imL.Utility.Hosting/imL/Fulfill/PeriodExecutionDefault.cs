using imL.Contract;

using System;
using System.Threading;

namespace imL.Utility.Hosting
{
    public class PeriodExecutionDefault : IPeriodExecution
    {
        private bool _DISPOSED;

        private long _COUNT;
        private IAppInfo _APP;
        private CancellationToken _TOKEN;

        private DateTime _START;
        private string _GUID;

        public long Count { get { return this._COUNT; } }
        public IAppInfo App { get { return this._APP; } }
        public CancellationToken Token { get { return this._TOKEN; } }

        public DateTime Start { get { return this._START; } }
        public string Guid { get { return this._GUID; } }

        public void PopulateWithSomething(long _count, IAppInfo _app, CancellationToken _token = default)
        {
            this._START = DateTime.Now;

            this._COUNT = _count;
            this._APP = _app;
            this._TOKEN = _token;

            this._GUID = Convert.ToString(System.Guid.NewGuid());
        }
        public void AfterPopulate()
        {
            //throw new NotImplementedException();
        }

        public string WorkingDoInfo()
        {
            return string.Format("{0} | {1}| {2}|", this._START.ToLocalTime(), this._GUID, this._COUNT);
        }

        public override string ToString()
        {
            return string.Format("[{0}]{1}|", this._COUNT, this._GUID);
        }

        //################################################################################
        ~PeriodExecutionDefault()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                //
            }

            this._DISPOSED = true;
        }
    }
}

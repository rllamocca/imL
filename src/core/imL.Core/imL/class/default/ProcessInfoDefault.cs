using System;
using System.Collections.Generic;

namespace imL
{
    public class ProcessInfoDefault : IProcessInfo
    {
        public DateTime? Start { set; get; }
        public string Guid { set; get; }
        public IAppInfo App { set; get; }
        public long? Selected { set; get; }
        public long? Inserted { set; get; }
        public long? Updated { set; get; }
        public long? Erased { set; get; }
        public long? Successes { set; get; }
        public long? Errors { set; get; }
        public IList<string> PathAttachments { set; get; } = new List<string>();
        public EAlert? Alert { set; get; }
        public Exception Critical { set; get; }
        public DateTime? End { set; get; }

        public ProcessInfoDefault(IAppInfo _info, bool _ala = false)
        {
            Start = DateTime.Now;
            Guid = Convert.ToString(System.Guid.NewGuid());
            App = _info;
        }

        public void AddSelected(long _add = 1)
        {
            if (_add > 0)
                Selected = Selected.GetValueOrDefault() + _add;
        }
        public void AddInserted(long _add = 1)
        {
            if (_add > 0)
                Inserted = Inserted.GetValueOrDefault() + _add;
        }
        public void AddUpdated(long _add = 1)
        {
            if (_add > 0)
                Updated = Updated.GetValueOrDefault() + _add;
        }
        public void AddErased(long _add = 1)
        {
            if (_add > 0)
                Erased = Erased.GetValueOrDefault() + _add;
        }
        public void AddSuccesses(long _add = 1)
        {
            if (_add > 0)
                Successes = Successes.GetValueOrDefault() + _add;
        }
        public void AddErrors(long _add = 1)
        {
            if (_add > 0)
                Errors = Errors.GetValueOrDefault() + _add;
        }

        public void Success()
        {
            long? _acum = Inserted.GetValueOrDefault() + Updated.GetValueOrDefault() + Erased.GetValueOrDefault();

            if (_acum == null || _acum == 0)
                Alert = EAlert.Info;

            if (Errors > 0)
                Alert = EAlert.Warning;

            if (Alert == EAlert.None)
                Alert = EAlert.Success;

            End = DateTime.Now;
        }
        public void Danger(Exception _ex = null)
        {
            Critical = _ex;
            Alert = EAlert.Danger;
            End = DateTime.Now;
        }
        public TimeSpan? Duration()
        {
            return End - Start;
        }
    }
}

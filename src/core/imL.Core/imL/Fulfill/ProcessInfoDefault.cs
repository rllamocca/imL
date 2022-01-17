using System;
using System.Collections.Generic;

using imL.Enumeration;

namespace imL.Contract
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
        public IList<string> PathAttachments { set; get; }
        public EAlert Alert { set; get; }
        public Exception Critical { set; get; }
        public DateTime? End { set; get; }

        public ProcessInfoDefault(IAppInfo _info)
        {
            this.Start = DateTime.Now;
            this.Guid = Convert.ToString(System.Guid.NewGuid());
            this.App = _info;
            this.PathAttachments = new List<string>();
        }

        public void AddSelected(long _add)
        {
            if (_add > 0)
                this.Selected = this.Selected.GetValueOrDefault() + _add;
        }
        public void AddInserted(long _add)
        {
            if (_add > 0)
                this.Inserted = this.Inserted.GetValueOrDefault() + _add;
        }
        public void AddUpdated(long _add)
        {
            if (_add > 0)
                this.Updated = this.Updated.GetValueOrDefault() + _add;
        }
        public void AddErased(long _add)
        {
            if (_add > 0)
                this.Erased = this.Erased.GetValueOrDefault() + _add;
        }
        public void Success()
        {
            long? _acum = this.Inserted.GetValueOrDefault() + this.Updated.GetValueOrDefault() + this.Erased.GetValueOrDefault();

            if (_acum == null || _acum == 0)
                this.Alert = EAlert.Warning;
            else
                this.Alert = EAlert.Success;

            this.End = DateTime.Now;
        }
        public void Danger(Exception _ex = null)
        {
            this.Critical = _ex;
            this.Alert = EAlert.Danger;
            this.End = DateTime.Now;
        }
        public TimeSpan? Duration()
        {
            return this.End - this.Start;
        }
    }
}

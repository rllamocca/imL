using imL.Enumeration;

using System;

namespace imL.Contract
{
    public class ProcessInfoDefault : IProcessInfo
    {
        public string Guid { set; get; }
        public DateTime? Start { set; get; }
        public DateTime? End { set; get; }
        public long? Selected { set; get; }
        public long? Inserted { set; get; }
        public long? Updated { set; get; }
        public long? Erased { set; get; }
        public EAlert Alert { set; get; }
        public Exception Critical { set; get; }

        public ProcessInfoDefault()
        {
            this.Start = DateTime.Now;
            this.Guid = Convert.ToString(System.Guid.NewGuid());
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

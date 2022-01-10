using imL.Enumeration;

using System;

namespace imL.Contract
{
    public interface IProcessInfo
    {

        string Guid { set; get; }
        DateTime? Start { set; get; }
        DateTime? End { set; get; }
        long? Selected { set; get; }
        long? Inserted { set; get; }
        long? Updated { set; get; }
        long? Erased { set; get; }
        EAlert Alert { set; get; }
        Exception Critical { set; get; }

        void AddSelected(long _add);
        void AddInserted(long _add);
        void AddUpdated(long _add);
        void AddErased(long _add);
        void Success();
        void Danger(Exception _ex = null);
        TimeSpan? Duration();
    }
}

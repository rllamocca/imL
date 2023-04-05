using System;
using System.Collections.Generic;

namespace imL
{
    public interface IProcessInfo
    {
        DateTime Start { set; get; }
        string Guid { set; get; }
        IAppInfo App { set; get; }
        long? Selected { set; get; }
        long? Inserted { set; get; }
        long? Updated { set; get; }
        long? Erased { set; get; }
        long? Successes { set; get; }
        long? Errors { set; get; }
        IList<string> PathAttachments { set; get; }
        EAlert? Alert { set; get; }
        Exception Critical { set; get; }
        DateTime? End { set; get; }

        void AddSelected(long _add = 1);
        void AddInserted(long _add = 1);
        void AddUpdated(long _add = 1);
        void AddErased(long _add = 1);
        void AddSuccesses(long _add = 1);
        void AddErrors(long _add = 1);

        void Success();
        void Danger(Exception _ex = null);
        TimeSpan? Duration();

        string Base { get; }
        string FileLog { get; }
        string BaseOut { get; }
    }
}

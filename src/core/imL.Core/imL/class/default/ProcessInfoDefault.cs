#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace imL
{
    public class ProcessInfoDefault : IProcessInfo
    {
        readonly string _PATH;
        readonly string _FILE_LOG;
        readonly string _PATH_OUT;

        public DateTime Start { set; get; }
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

        public string Base { get { return _PATH; } }
        public string FileLog { get { return _FILE_LOG; } }
        public string BaseOut { get { return _PATH_OUT; } }

        public ProcessInfoDefault(IAppInfo _info)
        {
            Start = DateTime.Now;
            Guid = Start.ToUniversalTime().ToString("yyyy'-'MM'-'dd' T'HH':'mm").Replace(":", "");
            _FILE_LOG = Start.ToString("yyyy'-'MM'-'dd' T'HH':'mm' 'zzz").Replace(":", "") + ".log";
            _PATH = Path.Combine(_info.BaseExe, Guid);
            _PATH_OUT = Path.Combine(_PATH, "out");

            if (Directory.Exists(_PATH) == false) Directory.CreateDirectory(_PATH);
            if (Directory.Exists(_PATH_OUT) == false) Directory.CreateDirectory(_PATH_OUT);

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


        public static string GetPID()
        {

#if NET50_OR_GREATER
            return Convert.ToString( Environment.ProcessId);
#else
            return Convert.ToString(Process.GetCurrentProcess().Id);
#endif

        }
    }
}

#endif
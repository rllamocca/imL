#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.IO;

using SYSTEM_IO = System.IO;

namespace imL
{
    public sealed class AppInfoDefault : IAppInfo
    {
        readonly string[] _ARGS;
        readonly string _PATH;
        readonly string _PATH_IN;
        readonly string _PATH_OUT;
        readonly string _PATH_LOG;
        readonly string _PATH_TMP;
        readonly bool _IN_CONTAINER = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        readonly bool _IN_TEMPPATH;

        public string[] Args { get { return _ARGS; } }
        public string Path { get { return _PATH; } }
        public string PathIn { get { return _PATH_IN; } }
        public string PathOut { get { return _PATH_OUT; } }
        public string PathLog { get { return _PATH_LOG; } }
        public string PathTmp { get { return _PATH_TMP; } }
        public bool InContainer { get { return _IN_CONTAINER; } }
        public bool InTempPath { get { return _IN_TEMPPATH; } }

        public AppInfoDefault(string[] _args, string _basedirectory = null, bool _temppathdefault = false)
        {
            _ARGS = _args;

#if NET35
            if (string.IsNullOrEmpty(_basedirectory.Trim()))
#else
            if (string.IsNullOrWhiteSpace(_basedirectory))
#endif
                _basedirectory = SYSTEM_IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app");

            _PATH = _basedirectory;
            _IN_TEMPPATH = _temppathdefault;
            _PATH_IN = SYSTEM_IO.Path.Combine(_PATH, "in");
            _PATH_OUT = SYSTEM_IO.Path.Combine(_PATH, "out");
            _PATH_LOG = SYSTEM_IO.Path.Combine(_PATH, "log");
            _PATH_TMP = _IN_TEMPPATH ? SYSTEM_IO.Path.GetTempPath() : SYSTEM_IO.Path.Combine(_PATH, "tmp");

            if (Directory.Exists(_PATH_IN) == false) Directory.CreateDirectory(_PATH_IN);
            if (Directory.Exists(_PATH_OUT) == false) Directory.CreateDirectory(_PATH_OUT);
            if (Directory.Exists(_PATH_LOG) == false) Directory.CreateDirectory(_PATH_LOG);
            if (Directory.Exists(_PATH_TMP) == false) Directory.CreateDirectory(_PATH_TMP);
        }

        //#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
        //        static string AssemblyDirectory()
        //        {
        //            string _cb = Assembly.GetExecutingAssembly().CodeBase;
        //            UriBuilder _uri = new UriBuilder(_cb);
        //            string _path = Uri.UnescapeDataString(_uri.Path);

        //            return Path.GetDirectoryName(_path);
        //        }
        //#endif
    }
}

#endif
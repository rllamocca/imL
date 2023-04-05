#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.IO;

namespace imL
{
    public sealed class AppInfoDefault : IAppInfo
    {
        readonly string[] _ARGS;
        readonly string _BASE;
        readonly string _BASE_IN;
        readonly string _BASE_EXE;
        readonly string _BASE_TMP;

        readonly bool? _IN_CONTAINER = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        readonly bool? _IN_TEMPPATH;

        public string[] args { get { return _ARGS; } }
        public string Base { get { return _BASE; } }
        public string BaseIn { get { return _BASE_IN; } }
        public string BaseExe { get { return _BASE_EXE; } }
        public string BaseTmp { get { return _BASE_TMP; } }

        public bool? InContainer { get { return _IN_CONTAINER; } }
        public bool? InTempPath { get { return _IN_TEMPPATH; } }

        public AppInfoDefault(string[] _args, string _basedirectory = null, bool _temppathdefault = false)
        {
            _ARGS = _args;

#if NET35
            if (string.IsNullOrEmpty(_basedirectory.Trim()))
#else
            if (string.IsNullOrWhiteSpace(_basedirectory))
#endif
                _basedirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app");

            _BASE = _basedirectory;
            _IN_TEMPPATH = _temppathdefault;

            _BASE_IN = Path.Combine(_BASE, "in");
            _BASE_EXE = Path.Combine(_BASE, "exe");
            _BASE_TMP = (_IN_TEMPPATH == true) ? Path.GetTempPath() : Path.Combine(_BASE, "tmp");

            if (Directory.Exists(_BASE_IN) == false) Directory.CreateDirectory(_BASE_IN);
            if (Directory.Exists(_BASE_EXE) == false) Directory.CreateDirectory(_BASE_EXE);
            if (Directory.Exists(_BASE_TMP) == false) Directory.CreateDirectory(_BASE_TMP);
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
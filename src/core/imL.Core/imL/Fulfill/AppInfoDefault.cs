#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.IO;

using imL.Contract;

using SYSTEM_IO = System.IO;

namespace imL.Fulfill
{
    public sealed class AppInfoDefault : IAppInfo
    {
        private readonly string[] _ARGS;
        private readonly string _PATH;
        private readonly string _PATH_IN;
        private readonly string _PATH_OUT;
        private readonly string _PATH_LOG;
        private readonly string _PATH_TMP;
        private readonly bool _IN_CONTAINER = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        private readonly bool _IN_TEMPPATH;

        public string[] Args { get { return this._ARGS; } }
        public string Path { get { return this._PATH; } }
        public string PathIn { get { return this._PATH_IN; } }
        public string PathOut { get { return this._PATH_OUT; } }
        public string PathLog { get { return this._PATH_LOG; } }
        public string PathTmp { get { return this._PATH_TMP; } }
        public bool InContainer { get { return this._IN_CONTAINER; } }
        public bool InTempPath { get { return this._IN_TEMPPATH; } }

        public AppInfoDefault(string[] _args, string _basedirectory = null, bool _temppathdefault = false)
        {
            this._ARGS = _args;

#if NET35
            if (string.IsNullOrEmpty(_basedirectory.Trim()))
#else
            if (string.IsNullOrWhiteSpace(_basedirectory))
#endif
                _basedirectory = SYSTEM_IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app");

            this._PATH = _basedirectory;
            this._IN_TEMPPATH = _temppathdefault;
            this._PATH_IN = SYSTEM_IO.Path.Combine(this._PATH, "in");
            this._PATH_OUT = SYSTEM_IO.Path.Combine(this._PATH, "out");
            this._PATH_LOG = SYSTEM_IO.Path.Combine(this._PATH, "log");
            this._PATH_TMP = this._IN_TEMPPATH ? SYSTEM_IO.Path.GetTempPath() : SYSTEM_IO.Path.Combine(this._PATH, "tmp");

            if (Directory.Exists(this._PATH_IN) == false) Directory.CreateDirectory(this._PATH_IN);
            if (Directory.Exists(this._PATH_OUT) == false) Directory.CreateDirectory(this._PATH_OUT);
            if (Directory.Exists(this._PATH_LOG) == false) Directory.CreateDirectory(this._PATH_LOG);
            if (Directory.Exists(this._PATH_TMP) == false) Directory.CreateDirectory(this._PATH_TMP);
        }

        //#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
        //        private static string AssemblyDirectory()
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
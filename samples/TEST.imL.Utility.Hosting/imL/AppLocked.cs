using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using NLog;

namespace TEST.imL.Utility.Hosting
{
    public static class AppLocked
    {
        private static readonly object _LOCKED = new();
        private static readonly Logger _LOGGER = LogManager.GetCurrentClassLogger();
        private static string _PATH_APP;
        private static string _PATH_APP_DATA;
        private static string _PATH_APP_LOG;
        private static string _PATH_APP_TMP;

        private static Settings _SETTING;
        private static HttpClient _HTTP;

        public static Logger Logger { get { lock (AppLocked._LOCKED) { return AppLocked._LOGGER; } } }
        public static string PathApp { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP; } } }
        public static string PathData { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP_DATA; } } }
        public static string PathLog { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP_LOG; } } }
        public static string PathTmp { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP_TMP; } } }

        public static Settings Setting { get { lock (AppLocked._LOCKED) { return AppLocked._SETTING; } } }
        public static HttpClient Http { get { lock (AppLocked._LOCKED) { return AppLocked._HTTP; } } }

        public static void Init(string[] _args, string _basedirectory = null, bool _tmpdefault = false)
        {
            if (string.IsNullOrWhiteSpace(_basedirectory))
                _basedirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app");

            AppLocked._PATH_APP = _basedirectory;
            AppLocked._PATH_APP_DATA = Path.Combine(AppLocked._PATH_APP, "data"); ;
            AppLocked._PATH_APP_LOG = Path.Combine(AppLocked._PATH_APP, "log"); ;
            if (_tmpdefault)
                AppLocked._PATH_APP_TMP = Path.GetTempPath();
            else
                AppLocked._PATH_APP_TMP = Path.Combine(AppLocked._PATH_APP, "tmp"); ;

            AppLocked._SETTING = JsonSerializer.Deserialize<Settings>(File.ReadAllText(Path.Combine(AppLocked._PATH_APP, "settings.json")));
            AppLocked._SETTING.Hosted.Args = _args;
            AppLocked._HTTP = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            //AppLocked._HTTP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _conf.User, _conf.Password))));
        }
    }
}

﻿using System;
using System.IO;
using System.Net.Http;
using System.ServiceModel;
using System.Text.Json;

using imL.Tool.Frotcom.ToGPSChile;

using NLog;

using SOAP_Registro;

namespace imL.Hosted.Frotcom.ToGPSChile
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
        private static RegistroClient _SOAP;

        public static Logger Logger { get { lock (AppLocked._LOCKED) { return AppLocked._LOGGER; } } }
        public static string PathApp { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP; } } }
        public static string PathData { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP_DATA; } } }
        public static string PathLog { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP_LOG; } } }
        public static string PathTmp { get { lock (AppLocked._LOCKED) { return AppLocked._PATH_APP_TMP; } } }

        public static Settings Setting { get { lock (AppLocked._LOCKED) { return AppLocked._SETTING; } } }
        public static HttpClient Http { get { lock (AppLocked._LOCKED) { return AppLocked._HTTP; } } }
        public static RegistroClient Soap { get { lock (AppLocked._LOCKED) { return AppLocked._SOAP; } } }


        public static void Init(string _basedirectory = null, bool _tmpdefault = false)
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
            AppLocked._HTTP = new HttpClient();

            AppLocked._SOAP = new();
            if (string.IsNullOrWhiteSpace(AppLocked._SETTING.Endpoint.Endpoint) == false)
                AppLocked._SOAP.Endpoint.Address = new EndpointAddress(AppLocked._SETTING.Endpoint.Endpoint);
        }
    }
}
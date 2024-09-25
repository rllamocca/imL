using System;
using System.IO;

namespace EmailDelivery;

internal class AppLock
{
    static readonly object _LOCKER = new();

    static readonly string _PATH_JSON;

    internal static string PathJson { get { lock (_LOCKER) return _PATH_JSON; } }

    static AppLock()
    {
        string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app");
        _PATH_JSON = Path.Combine(_path, "settings.json");

        if (File.Exists(_PATH_JSON) == false)
        {
            Directory.CreateDirectory(_path);
            File.WriteAllText(_PATH_JSON, "{}");
        }

        





    }
}

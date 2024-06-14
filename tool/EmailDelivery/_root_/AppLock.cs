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
        _PATH_JSON =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app", "settings.json");
    }
}

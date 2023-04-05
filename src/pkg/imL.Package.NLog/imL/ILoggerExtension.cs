using System;

using NLog;

namespace imL.Package.NLog
{
    public static class ILoggerExtension
    {
        public static ILogger LetTrace(this ILogger _this)
        {
            if (_this == null)
                return null;

            if (_this.IsEnabled(LogLevel.Trace))
                return _this;

            return null;
        }
        public static ILogger LetDebug(this ILogger _this)
        {
            if (_this == null)
                return null;

            if (_this.IsEnabled(LogLevel.Debug))
                return _this;

            return null;
        }
        public static ILogger LetInformation(this ILogger _this)
        {
            if (_this == null)
                return null;

            if (_this.IsEnabled(LogLevel.Info))
                return _this;

            return null;
        }
        public static ILogger LetWarning(this ILogger _this)
        {
            if (_this == null)
                return null;

            if (_this.IsEnabled(LogLevel.Warn))
                return _this;

            return null;
        }
        public static ILogger LetError(this ILogger _this)
        {
            if (_this == null)
                return null;

            if (_this.IsEnabled(LogLevel.Error))
                return _this;

            return null;
        }
        public static ILogger LetCritical(this ILogger _this)
        {
            if (_this == null)
                return null;

            if (_this.IsEnabled(LogLevel.Fatal))
                return _this;

            return null;
        }

        public static void InnerFatal(this ILogger _this, Exception _ex)
        {
            if (_this == null)
                return;

            while (_ex != null)
            {
                _this.Fatal(_ex);
                _ex = _ex.InnerException;
            }
        }
    }
}

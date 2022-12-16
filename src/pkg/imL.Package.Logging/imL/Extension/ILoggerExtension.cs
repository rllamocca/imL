using System;

using Microsoft.Extensions.Logging;

namespace imL.Package.Logging
{
    public static class ILoggerExtension
    {
        public static ILogger
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ?
#endif
            LetTrace(this ILogger _this)
        {
            if (_this == null)
                throw new ArgumentNullException(nameof(_this));

            if (_this.IsEnabled(LogLevel.Trace))
                return _this;

            return null;
        }
        public static ILogger
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ?
#endif
            LetDebug(this ILogger _this)
        {
            if (_this == null)
                throw new ArgumentNullException(nameof(_this));

            if (_this.IsEnabled(LogLevel.Debug))
                return _this;

            return null;
        }
        public static ILogger
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ?
#endif
            LetInformation(this ILogger _this)
        {
            if (_this == null)
                throw new ArgumentNullException(nameof(_this));

            if (_this.IsEnabled(LogLevel.Information))
                return _this;

            return null;
        }
        public static ILogger
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ?
#endif
            LetWarning(this ILogger _this)
        {
            if (_this == null)
                throw new ArgumentNullException(nameof(_this));

            if (_this.IsEnabled(LogLevel.Warning))
                return _this;

            return null;
        }
        public static ILogger
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ?
#endif
            LetError(this ILogger _this)
        {
            if (_this == null)
                throw new ArgumentNullException(nameof(_this));

            if (_this.IsEnabled(LogLevel.Error))
                return _this;

            return null;
        }
        public static ILogger
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ?
#endif
            LetCritical(this ILogger _this)
        {
            if (_this == null)
                throw new ArgumentNullException(nameof(_this));

            if (_this.IsEnabled(LogLevel.Critical))
                return _this;

            return null;
        }

        public static void InnerLogCritical(this ILogger _this, Exception _ex)
        {
            if (_this == null)
                throw new ArgumentNullException(nameof(_this));

            while (_ex != null)
            {
                _this.LogCritical(_ex, "{p0}", _ex.Message);
                _ex = _ex.InnerException;
            }
        }
    }
}

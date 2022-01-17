using Microsoft.Extensions.Logging;

namespace imL.Package.Logging
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

            if (_this.IsEnabled(LogLevel.Information))
                return _this;

            return null;
        }
        public static ILogger LetWarning(this ILogger _this)
        {
            if (_this == null)
                return null;

            if (_this.IsEnabled(LogLevel.Warning))
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

            if (_this.IsEnabled(LogLevel.Critical))
                return _this;

            return null;
        }
    }
}

using Microsoft.Extensions.Logging;

namespace imL.Utility.Logging
{
    public static class LoggingExtension
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
    }
}

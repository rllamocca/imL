using System;
using System.Collections.Generic;
using System.Linq;

namespace imL
{
    public static class ExceptionExtension
    {
        public static IEnumerable<string> InnerMessageExceptionISync(this Exception _this)
        {
            while (_this != null)
            {
                yield return _this.Message;

                _this = _this.InnerException;
            }
        }
        public static IEnumerable<string> InnerMessageException(this Exception _this)
        {
            return _this.InnerMessageExceptionISync().ToArray();
        }

        public static IEnumerable<Exception> InnerExceptionISync(this Exception _this)
        {
            while (_this != null)
            {
                yield return _this;

                _this = _this.InnerException;
            }
        }
        public static IEnumerable<Exception> InnerException(this Exception _this)
        {
            return _this.InnerExceptionISync().ToArray();
        }
    }
}

using System;
using System.Collections.Generic;

namespace imL
{
    public static class ExceptionHelper
    {
        public static IEnumerable<string> InnerException(Exception _ex)
        {
            IList<string> _return = new List<string>();

            while (_ex != null)
            {
                _return.Add(_ex.Message);
                _ex = _ex.InnerException;
            }

            return _return;
        }
    }
}

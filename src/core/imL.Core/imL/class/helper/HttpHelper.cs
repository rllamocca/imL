using System;

namespace imL
{
    public class HttpHelper
    {
        public static string CheckHttp(string _from, string _to)
        {
            string _https = "https://";
            string _http = "http://";
            StringComparison _c = StringComparison.OrdinalIgnoreCase;

            if (
                (_from.StartsWith(_https, _c) && _to.StartsWith(_https, _c)) ||
                (_from.StartsWith(_http, _c) && _to.StartsWith(_http, _c))
                )
                return _to;

            if (_from.StartsWith(_https, _c))
                return _to.Replace(_http, _https);

            if (_from.StartsWith(_http, _c))
                return _to.Replace(_https, _http);

            return _to;
        }
    }
}

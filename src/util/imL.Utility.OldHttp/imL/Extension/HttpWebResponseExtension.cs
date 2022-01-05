#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System.IO;
using System.Net;

namespace imL.Utility.OldHttp
{
    public static class HttpWebResponseExtension
    {
        public static string ReadAsString(this HttpWebResponse _this)
        {
            string _body;

            using (StreamReader _sr = new StreamReader(_this.GetResponseStream()))
                _body = _sr.ReadToEnd();

            return _body;
        }

        public static T ReadAsObject<T>(this HttpWebResponse _this)
        {
            string _body = _this.ReadAsString();

            if (string.IsNullOrEmpty(_body))
                return default;

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
            return JsonSerializer.Deserialize<T>(_body);
#else
            return JsonConvert.DeserializeObject<T>(_body);
#endif
        }
    }
}

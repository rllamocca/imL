#if (NET35 || NET40) == false
using System.Net.Http;
using System.Threading.Tasks;
#else
using System.IO;
using System.Net;
#endif

using Newtonsoft.Json;

namespace imL.Utility.Http.Newtonsoft_Json
{
    public static class HttpResponseMessage_imLUtilityHttpNewtonsoft_JsonExtension
    {

#if (NET35 || NET40) == false

        public async static Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage _this)
        {
            _this.EnsureSuccessStatusCode();
            string _body = await _this.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(_body))
                return default;

            return JsonConvert.DeserializeObject<T>(_body);
        }

#else

        public static T ReadAsJson<T>(this HttpWebResponse _this)
        {
            string _body;
            using (StreamReader _sr = new StreamReader(_this.GetResponseStream()))
                _body = _sr.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(_body);
        }

#endif

    }
}

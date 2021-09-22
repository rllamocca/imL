using System.IO;
using System.Net;

using Newtonsoft.Json;

namespace imL.Utility.OldHttp
{
    public static class HttpWebResponse_imLUtilityOldHttpExtension
    {
        public static T ReadAsJson<T>(this HttpWebResponse _this)
        {
            string _body;

            using (StreamReader _sr = new StreamReader(_this.GetResponseStream()))
                _body = _sr.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(_body);
        }
    }
}

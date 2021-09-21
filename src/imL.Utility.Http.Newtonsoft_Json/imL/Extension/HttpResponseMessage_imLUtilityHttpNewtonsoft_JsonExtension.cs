﻿#if (NET35 || NET40) == false

using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace imL.Utility.Http.Newtonsoft_Json
{
    public static class HttpResponseMessage_imLUtilityHttpNewtonsoft_JsonExtension
    {
        public async static Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage _this)
        {
            _this.EnsureSuccessStatusCode();
            string _body = await _this.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(_body))
                return default;

            return JsonConvert.DeserializeObject<T>(_body);
        }
    }
}

#endif
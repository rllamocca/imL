#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System.Net.Http;
using System.Threading.Tasks;

namespace imL.Utility.Http
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<string> ReadAsStringAsync(this HttpResponseMessage _this)
        {
            _this.EnsureSuccessStatusCode();

            return await _this.Content.ReadAsStringAsync();
        }

        public static async Task<T> ReadAsObjectAsync<T>(this HttpResponseMessage _this)
        {
            string _body = await _this.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(_body))
                return default;

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
            return JsonSerializer.Deserialize<T>(_body);
#else
            return JsonConvert.DeserializeObject<T>(_body);
#endif

        }
    }
}

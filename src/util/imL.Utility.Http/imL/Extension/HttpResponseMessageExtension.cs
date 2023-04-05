#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Net.Http.Json;
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace imL.Utility.Http
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<string> ReadAsStringAsync(this HttpResponseMessage _this, bool _essc = true, CancellationToken _ct = default)
        {
            if (_essc)
                _this.EnsureSuccessStatusCode();

#if NET5_0_OR_GREATER
            return await _this.Content.ReadAsStringAsync(_ct);
#else
            return await _this.Content.ReadAsStringAsync();
#endif
        }

        public static async Task<G> ReadJsonAsync<G>(this HttpResponseMessage _this, bool _essc = true, CancellationToken _ct = default)
        {
            if (_essc)
                _this.EnsureSuccessStatusCode();

#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP
            JsonSerializerOptions _null = null;

            return await _this.Content.ReadFromJsonAsync<G>(_null, _ct);
#else
            string _body = await _this.ReadAsStringAsync(false, _ct);

            if (string.IsNullOrWhiteSpace(_body))
                return default;

            return JsonConvert.DeserializeObject<G>(_body);
#endif
        }
    }
}

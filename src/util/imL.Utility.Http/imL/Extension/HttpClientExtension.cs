#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP
using System.Text.Json;
using System.Net.Http.Json;
#endif

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace imL.Utility.Http
{
    public static class HttpClientExtension
    {
        static async Task<G> SendJsonAsync<G>(this HttpClient _this, HttpMethod _method, string _uri, HttpContent _content = null, bool _essc = true, CancellationToken _ct = default)
        {
            using (HttpRequestMessage _req = new HttpRequestMessage(_method, _uri))
            {
                HttpJsonHelper.DefaultAccept(_req);
                _req.Content = _content ?? _req.Content;

                using (HttpResponseMessage _res = await _this.SendAsync(_req, _ct))
                    return await _res.ReadJsonAsync<G>(_essc, _ct);
            }
        }

        public static async Task<G> GetJsonAsync<G>(this HttpClient _this, string _uri, bool _essc = true, CancellationToken _ct = default)
        {
#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP
            return await _this.GetFromJsonAsync<G>(_uri, _ct);
#else
            return await _this.SendJsonAsync<G>(HttpMethod.Get, _uri, null, _essc, _ct);
#endif
        }
        public static async Task<G> DeleteJsonAsync<G>(this HttpClient _this, string _uri, bool _essc = true, CancellationToken _ct = default)
        {
#if NETSTANDARD2_0_OR_GREATER || NET7_0_OR_GREATER
            return await _this.DeleteFromJsonAsync<G>(_uri, _ct);
#else
            return await _this.SendJsonAsync<G>(HttpMethod.Delete, _uri, null, _essc, _ct);
#endif
        }

        public static async Task<G> PostJsonAsync<G>(this HttpClient _this, string _uri, HttpContent _content = null, bool _essc = true, CancellationToken _ct = default)
        {
            return await _this.SendJsonAsync<G>(HttpMethod.Post, _uri, _content, _essc, _ct);
        }
        public static async Task<G> PutJsonAsync<G>(this HttpClient _this, string _uri, HttpContent _content = null, bool _essc = true, CancellationToken _ct = default)
        {
            return await _this.SendJsonAsync<G>(HttpMethod.Put, _uri, _content, _essc, _ct);
        }

        public static async Task<GRs> PostJsonAsync<GRs, GRq>(this HttpClient _this, string _uri, GRq _value, bool _essc = true, CancellationToken _ct = default)
        {
#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP
            //JsonSerializerOptions _null = null;

            using (HttpResponseMessage _res = await _this.PostAsJsonAsync(_uri, _value, _ct))
                return await _res.ReadJsonAsync<GRs>(_essc, _ct);
#else
            return await _this.PostJsonAsync<GRs>(_uri, HttpJsonHelper.JsonContent(_value), _essc, _ct);
#endif
        }
        public static async Task<GRs> PutJsonAsync<GRs, GRq>(this HttpClient _this, string _uri, GRq _value, bool _essc = true, CancellationToken _ct = default)
        {
#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP
            using (HttpResponseMessage _res = await _this.PutAsJsonAsync(_uri, _value, _ct))
                return await _res.ReadJsonAsync<GRs>(_essc, _ct);
#else
            return await _this.PutJsonAsync<GRs>(_uri, HttpJsonHelper.JsonContent(_value), _essc, _ct);
#endif
        }
    }
}

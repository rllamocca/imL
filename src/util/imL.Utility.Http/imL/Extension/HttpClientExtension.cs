using System.Net.Http;
using System.Threading.Tasks;

namespace imL.Utility.Http
{
    public static class HttpClientExtension
    {
        public static async Task<T> GetAsync<T>(this HttpClient _this, string _uri)
        {
            using (HttpRequestMessage _req = new HttpRequestMessage(HttpMethod.Get, _uri))
            {
                HttpJsonHelper.DefaultAccept(_req);

                using (HttpResponseMessage _res = await _this.SendAsync(_req))
                    return await _res.ReadAsObjectAsync<T>();
            }
        }
        public static async Task<T> PostAsync<T>(this HttpClient _this, string _uri, HttpContent _content = null)
        {
            using (HttpRequestMessage _req = new HttpRequestMessage(HttpMethod.Post, _uri))
            {
                HttpJsonHelper.DefaultAccept(_req);
                _req.Content = _content;

                using (HttpResponseMessage _res = await _this.SendAsync(_req))
                    return await _res.ReadAsObjectAsync<T>();
            }
        }
        public static async Task<T> PutAsync<T>(this HttpClient _this, string _uri, HttpContent _content = null)
        {
            using (HttpRequestMessage _req = new HttpRequestMessage(HttpMethod.Put, _uri))
            {
                HttpJsonHelper.DefaultAccept(_req);
                _req.Content = _content;

                using (HttpResponseMessage _res = await _this.SendAsync(_req))
                    return await _res.ReadAsObjectAsync<T>();
            }
        }
    }
}

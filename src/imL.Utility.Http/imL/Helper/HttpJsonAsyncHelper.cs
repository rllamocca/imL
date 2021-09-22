using System.Net.Http;
using System.Threading.Tasks;

using imL.Enumeration.Http;

namespace imL.Utility.Http
{
    public static class HttpJsonAsyncHelper
    {
        public async static Task<T> Post<T>(HttpClient _client, string _url, object _post, ECompress _compress = ECompress.None)
        {
            using (HttpRequestMessage _req = new HttpRequestMessage(HttpMethod.Post, _url))
            {
                HttpJsonHelper.DefaultAccept(_req, _compress);

                using (HttpContent _content = HttpJsonHelper.JsonContent(_post, _compress))
                {
                    _req.Content = _content;

                    using (HttpResponseMessage _res = await _client.SendAsync(_req))
                        return await _res.ReadAsJsonAsync<T>();
                }
            }
        }

        public async static Task<T> Put<T>(HttpClient _client, string _url, object _put, ECompress _compress = ECompress.None)
        {
            using (HttpRequestMessage _req = new HttpRequestMessage(HttpMethod.Put, _url))
            {
                HttpJsonHelper.DefaultAccept(_req, _compress);

                using (HttpContent _content = HttpJsonHelper.JsonContent(_put, _compress))
                {
                    _req.Content = _content;

                    using (HttpResponseMessage _res = await _client.SendAsync(_req))
                        return await _res.ReadAsJsonAsync<T>();
                }
            }
        }
    }
}
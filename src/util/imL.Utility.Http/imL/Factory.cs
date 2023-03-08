using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace imL.Utility.Http
{
    public static class Factory
    {
        public static HttpClient HttpJsonClient(string _baseaddress = null)
        {
            HttpClient _CLIENT = new HttpClient(new HttpJsonHandler(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }));
            _CLIENT.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_baseaddress != null)
                _CLIENT.BaseAddress = new Uri(_baseaddress);

            return _CLIENT;
        }
    }
}

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace imL.Utility.Http
{
    public class HttpJsonHandler : DelegatingHandler
    {
        public HttpJsonHandler(HttpMessageHandler _innerHandler)
           : base(_innerHandler) { }

#if NETCOREAPP
        protected override HttpResponseMessage Send(HttpRequestMessage _rq, CancellationToken _ct)
        {
            _rq.Content?.ReadAsStream(_ct);

            return base.Send(_rq, _ct);
        }
#endif
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage _rq, CancellationToken _ct)
        {
            if (_rq.Content != null)
#if NETCOREAPP
                await _rq.Content.ReadAsStreamAsync(_ct);
#else
                await _rq.Content.ReadAsStreamAsync();
#endif

            return await base.SendAsync(_rq, _ct);
        }
    }
}

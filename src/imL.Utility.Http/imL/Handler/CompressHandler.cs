using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace imL.Utility.Http.Handler
{
    public class CompressHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage _request, CancellationToken _token)
        {
            await HttpAsyncHelper.DecompressContent(_request);

            HttpResponseMessage _response = await base.SendAsync(_request, _token);

            await HttpAsyncHelper.CompressContent(_response, _request.Headers.AcceptEncoding);

            return _response;
        }
    }
}
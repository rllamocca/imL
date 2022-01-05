using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace imL.Utility.Http
{
    public class CompressAsyncHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage _request, CancellationToken _token)
        {
            await HttpHelperAsync.DecompressContent(_request);

            HttpResponseMessage _response = await base.SendAsync(_request, _token);

            await HttpHelperAsync.CompressContent(_response, _request.Headers.AcceptEncoding);

            return _response;
        }
    }
}
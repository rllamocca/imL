#if (NET35 || NET40) == false

using System.Net.Http;
using System.Net.Http.Headers;

using imL.Enumeration.Http;

namespace imL.Utility.Http
{
    public static class HttpHelper
    {
        public static void DefaultAccept(HttpRequestMessage _req, ECompress _compress = ECompress.None)
        {
            _req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            switch (_compress)
            {
                case ECompress.Gzip:
                    _req.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    break;
                case ECompress.Deflate:
                    _req.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                    break;
                default:
                    break;
            }
        }
    }
}

#endif
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

using imL.Http;
using imL.Package.Json;

namespace imL.Utility.Http
{
    public static class HttpJsonHelper
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

        public static HttpContent JsonContent(object _obj, ECompress _compress = ECompress.None)
        {
            JsonHelper.ToJsonStream(out Stream _ms, _obj);

            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _ms.CheckBeginPosition();
                    _ms = StreamHelper.Compress(_ms, _compress);

                    break;
                default:

                    break;
            }

            _ms.CheckBeginPosition();
            HttpContent _return = new StreamContent(_ms);
            _return.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _return.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("charset", "utf-8"));

            switch (_compress)
            {
                case ECompress.Gzip:
                    _return.Headers.Add("Content-Encoding", "gzip");

                    break;
                case ECompress.Deflate:
                    _return.Headers.Add("Content-Encoding", "deflate");

                    break;
                default:
                    break;
            }

            return _return;
        }
    }
}
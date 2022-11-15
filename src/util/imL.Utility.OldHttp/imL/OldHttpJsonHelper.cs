using System.IO;
using System.Net;

using imL.Enumeration.Http;
using imL.Package.Json;

namespace imL.Utility.OldHttp
{
    public static class OldHttpJsonHelper
    {
        public static void DefaultAccept(HttpWebRequest _req, ECompress _compress = ECompress.None)
        {
            _req.Accept = "application/json";

            switch (_compress)
            {
                case ECompress.Gzip:
                    _req.Headers.Add("Accept-Encoding", "gzip");

                    break;
                case ECompress.Deflate:
                    _req.Headers.Add("Accept-Encoding", "deflate");

                    break;
                default:
                    break;
            }
        }
        public static void JsonContent(HttpWebRequest _client, object _obj = null, ECompress _compress = ECompress.None)
        {
            if (_obj == null)
                return;

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

            using (Stream _req = _client.GetRequestStream())
            {
#if (NET35)
                _ms.CopyTo(_req);
#else
                _ms.CopyTo(_req);
#endif
            }

            _client.ContentType = "application/json; charset=utf-8";

            switch (_compress)
            {
                case ECompress.Gzip:
                    _client.Headers.Add("Content-Encoding", "gzip");

                    break;
                case ECompress.Deflate:
                    _client.Headers.Add("Content-Encoding", "deflate");

                    break;
                default:
                    break;
            }
        }
    }
}
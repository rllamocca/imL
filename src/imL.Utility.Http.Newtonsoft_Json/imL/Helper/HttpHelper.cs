#if (NET35 || NET40) == false
using System.Net.Http;
using System.Net.Http.Headers;
#endif

using System.Net;

using imL.Enumeration.Http;

namespace imL.Utility.Http
{
    public static class HttpHelper
    {
#if (NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false

        public static void OldDefaultAccept(HttpWebRequest _req, ECompress _compress = ECompress.None)
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

#endif

#if (NET35 || NET40) == false

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
#endif

    }
}

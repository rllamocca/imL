using System.IO;
using System.Net;

using imL.Enumeration.Http;
using imL.Utility.Newtonsoft_Json;

namespace imL.Utility.OldHttp
{
    public static class HttpJsonHelper
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
        public static void JsonContent(HttpWebRequest _client, object _obj, ECompress _compress = ECompress.None)
        {
            Stream _ms;

            if (_obj is string _string)
                _ms = NewtonsoftHelper.ToStream(_string);
            else
                _ms = NewtonsoftHelper.ToStream(_obj);

            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _ms = StreamHelper.Compress(_ms, _compress);
                    break;
                default:
                    break;
            }

            using (Stream _s = _client.GetRequestStream())
            {
#if (NET35)
                _ms.OldCopyTo(_s);
#else
                _ms.CopyTo(_s);
#endif
            }

            _client.ContentLength = _ms.Length;
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

        public static T Post<T>(HttpWebRequest _client, object _post, ECompress _compress = ECompress.None)
        {
            _client.Method = "POST";
            HttpJsonHelper.DefaultAccept(_client);
            HttpJsonHelper.JsonContent(_client, _post, _compress);

            return ((HttpWebResponse)_client.GetResponse()).ReadAsJson<T>();
        }
        public static T Put<T>(HttpWebRequest _client, object _put, ECompress _compress = ECompress.None)
        {
            _client.Method = "PUT";
            HttpJsonHelper.DefaultAccept(_client);
            HttpJsonHelper.JsonContent(_client, _put, _compress);

            return ((HttpWebResponse)_client.GetResponse()).ReadAsJson<T>();
        }
    }
}
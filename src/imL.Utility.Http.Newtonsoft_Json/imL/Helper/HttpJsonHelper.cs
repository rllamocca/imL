#if (NET35 || NET40) == false

using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

using imL.Enumeration.Http;
using imL.Utility.Newtonsoft_Json;

namespace imL.Utility.Http.Newtonsoft_Json
{
    public static class HttpJsonHelper
    {
        public static HttpContent JsonContent(object _obj, ECompress _compress = ECompress.None)
        {
            if (_obj == null)
                return null;

            Stream _ms = NewtonsoftHelper.ToJsonToStream(_obj);

            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _ms = StreamHelper.Compress(_ms, _compress);
                    break;
                default:
                    break;
            }

            HttpContent _return = new StreamContent(_ms);
            _return.Headers.ContentType = new MediaTypeHeaderValue("application/json");

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

#endif
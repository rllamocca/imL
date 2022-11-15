using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using imL.Enumeration.Http;
using imL.Format;

namespace imL.Utility.Http
{
    public static class HttpHelperAsync
    {
        public static async Task CompressContentAsync(HttpResponseMessage _res, HttpHeaderValueCollection<StringWithQualityHeaderValue> _ae)
        {
            if (_res.Content != null)
            {
                ECompress _compress = ECompress.None;
                if (_ae.Contains(new StringWithQualityHeaderValue("deflate"))) _compress = ECompress.Deflate;
                if (_ae.Contains(new StringWithQualityHeaderValue("gzip"))) _compress = ECompress.Gzip;

                switch (_compress)
                {
                    case ECompress.Gzip:
                    case ECompress.Deflate:
                        using (Stream _content = await _res.Content.ReadAsStreamAsync())
                        {
                            StreamContent _response = new StreamContent(StreamHelper.Compress(_content, _compress));

                            foreach (KeyValuePair<string, IEnumerable<string>> _item in _res.Content.Headers)
                                if (_item.Key != "Content-Length")
                                    _response.Headers.TryAddWithoutValidation(_item.Key, _item.Value);

                            _response.Headers.ContentEncoding.Add((_compress == ECompress.Gzip) ? "gzip" : "deflate");

                            _res.Content = _response;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public static async Task DecompressContentAsync(HttpRequestMessage _req)
        {
            if (_req.Content != null)
            {
                ICollection<string> _ce = _req.Content.Headers.ContentEncoding;
                ECompress _decompress = ECompress.None;
                if (_ce.Contains("deflate")) _decompress = ECompress.Deflate;
                if (_ce.Contains("gzip")) _decompress = ECompress.Gzip;

                switch (_decompress)
                {
                    case ECompress.Gzip:
                    case ECompress.Deflate:
                        using (Stream _content = await _req.Content.ReadAsStreamAsync())
                        {
                            StreamContent _request = new StreamContent(StreamHelper.Decompress(_content, _decompress));

                            foreach (KeyValuePair<string, IEnumerable<string>> _item in _req.Content.Headers)
                                if (_item.Key != "Content-Length")
                                    _request.Headers.TryAddWithoutValidation(_item.Key, _item.Value);

                            _request.Headers.ContentEncoding.Remove((_decompress == ECompress.Gzip) ? "gzip" : "deflate");

                            _req.Content = _request;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public static AuthenticationHeaderValue Authentication(EndpointFormat _format)
        {
            string _name = Convert.ToString(_format.Scheme);

            switch (_format.Scheme)
            {
                case EAuthentication.Basic:
                    byte[] _unicode = Encoding.Unicode.GetBytes(string.Format("{0}:{1}", _format.Key, _format.Value));

#if (NETSTANDARD1_1 || NETSTANDARD1_2)
                    Encoding _dest = Encoding.UTF8;
#else
                    Encoding _dest = Encoding.ASCII;
#endif

                    byte[] _ascii = Encoding.Convert(Encoding.Unicode, _dest, _unicode);

                    return new AuthenticationHeaderValue(_name, Convert.ToBase64String(_ascii));
                case EAuthentication.Bearer:
                case EAuthentication.Token:
                    return new AuthenticationHeaderValue(_name, _format.Key);
                case EAuthentication.Digest:
                    throw new NotImplementedException();
                default:
                    break;
            }

            return null;
        }
        public static AuthenticationHeaderValue Authentication(EAuthentication _scheme, string _key, string _value = null)
        {
            EndpointFormat _format = new EndpointFormat
            {
                Scheme = _scheme,
                Key = _key,
                Value = _value
            };

            return Authentication(_format);
        }
    }
}
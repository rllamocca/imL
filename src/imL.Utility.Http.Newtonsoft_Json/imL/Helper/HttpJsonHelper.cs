#if (NET35 || NET40) == false
using System.Net.Http;
using System.Net.Http.Headers;
#else
using System.Net;

using Newtonsoft.Json;
#endif

using System.IO;

using imL.Enumeration.Http;
using imL.Utility.Newtonsoft_Json;

namespace imL.Utility.Http.Newtonsoft_Json
{
    public static class HttpJsonHelper
    {

#if (NET35 || NET40) == false

        public static HttpContent JsonContent(object _obj, ECompress _compress = ECompress.None)
        {
            Stream _ms = NewtonsoftHelper.ToStream(_obj);

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

#else

        public static void OldJsonContent(HttpWebRequest _client, object _obj, ECompress _compress = ECompress.None)
        {
            Stream _ms = NewtonsoftHelper.ToStream(_obj);

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

        public static T OldPost<T>(HttpWebRequest _client, object _post, ECompress _compress = ECompress.None)
        {
            _client.Method = "POST";
            HttpHelper.OldDefaultAccept(_client);
            OldJsonContent(_client, _post, _compress);
            HttpWebResponse _hwre = (HttpWebResponse)_client.GetResponse();
            string _body;
            using (StreamReader _sr = new StreamReader(_hwre.GetResponseStream()))
                _body = _sr.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(_body);
        }
        public static T OldPut<T>(HttpWebRequest _client, object _put, ECompress _compress = ECompress.None)
        {
            _client.Method = "PUT";
            HttpHelper.OldDefaultAccept(_client);
            OldJsonContent(_client, _put, _compress);
            HttpWebResponse _hwre = (HttpWebResponse)_client.GetResponse();
            string _body;
            using (StreamReader _sr = new StreamReader(_hwre.GetResponseStream()))
                _body = _sr.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(_body);
        }

#endif

    }
}


/*
public class JsonContent : HttpContent
   {
    private readonly MemoryStream _stream = new MemoryStream();
    ~JsonContent()
    {
        _stream.Dispose();
    }

    public JsonContent(object value)
    {
        Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using (var contexStream = new MemoryStream())
        using (var jw = new JsonTextWriter(new StreamWriter(contexStream)) { Formatting = Formatting.Indented })
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, value);
            jw.Flush();
            contexStream.Position = 0;
            contexStream.WriteTo(_stream);
        }
        _stream.Position = 0;

    }

    private JsonContent(string content)
    {
        Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using (var contexStream = new MemoryStream())
        using (var sw = new StreamWriter(contexStream))
        {
            sw.Write(content);
            sw.Flush();
            contexStream.Position = 0;
            contexStream.WriteTo(_stream);
        }
        _stream.Position = 0;
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
    {
        return _stream.CopyToAsync(stream);
    }

    protected override bool TryComputeLength(out long length)
    {
        length = _stream.Length;
        return true;
    }

    public static HttpContent FromFile(string filepath)
    {
        var content = File.ReadAllText(filepath);
        return new JsonContent(content);
    }
    public string ToJsonString()
    {
        return Encoding.ASCII.GetString(_stream.GetBuffer(), 0, _stream.GetBuffer().Length).Trim();
    }
}
*/
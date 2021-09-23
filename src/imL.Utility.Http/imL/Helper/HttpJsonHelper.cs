using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

using imL.Enumeration.Http;
using imL.Utility.Newtonsoft_Json;

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
            Stream _ms;

            if (_obj is string _string)
                NewtonsoftHelper.ToStream(out _ms, _string);
            else
                NewtonsoftHelper.ToStream(out _ms, _obj);

            _ms.CheckBeginPosition();

            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _ms = StreamHelper.Compress(_ms, _compress);
                    _ms.CheckBeginPosition();

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
#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System.IO;
using System.Text;

using imL.Utility;

namespace imL.Package.Json
{
    public static class JsonHelper
    {
        public static void ToJsonStream(out Stream _out, object _obj, Encoding _enc = null)
        {
            if (_obj is string _string)
                _string.ToStream(out _out, _enc);
            else
            {
                ReadOnly.DefaultEncoding_NoBOM(ref _enc);

                _out = new MemoryStream();

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
                Utf8JsonWriter _jw = new Utf8JsonWriter(_out, new JsonWriterOptions() { Indented = false });
                JsonDocument _doc = JsonDocument.Parse(JsonSerializer.Serialize(_obj));
                JsonElement _ele = _doc.RootElement;

                if (_ele.ValueKind == JsonValueKind.Object)
                    _jw.WriteStartObject();
                else
                    return;

                foreach (JsonProperty _item in _ele.EnumerateObject())
                    _item.WriteTo(_jw);

                _jw.WriteEndObject();
                _jw.Flush();
#else
                StreamWriter _sw = new StreamWriter(_out, _enc);
                JsonTextWriter _jtw = new JsonTextWriter(_sw) { Formatting = Formatting.None };
                JsonSerializer _js = new JsonSerializer();
                _js.Serialize(_jtw, _obj);
                _sw.Flush();
#endif

            }
        }
    }
}
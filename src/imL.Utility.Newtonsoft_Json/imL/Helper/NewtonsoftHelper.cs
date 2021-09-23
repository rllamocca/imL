using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace imL.Utility.Newtonsoft_Json
{
    public static class NewtonsoftHelper
    {
        public static void ToStream(out Stream _out, object _obj, Encoding _enc = null)
        {
            ReadOnly.DefaultEncoding_NoBOM(ref _enc);

            _out = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_out, _enc);
            JsonTextWriter _jtw = new JsonTextWriter(_sw) { Formatting = Formatting.None };
            JsonSerializer _js = new JsonSerializer();
            _js.Serialize(_jtw, _obj);
            _sw.Flush();
        }

        public static void ToStream(out Stream _out, string _string, Encoding _enc = null)
        {
            ReadOnly.DefaultEncoding_NoBOM(ref _enc);

            _out = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_out, _enc);
            _sw.Write(_string);
            _sw.Flush();
        }
    }
}
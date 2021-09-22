using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace imL.Utility.Newtonsoft_Json
{
    public static class NewtonsoftHelper
    {
        public static Stream ToStream(object _obj, Encoding _enc = null)
        {
            ReadOnly.DefaultEncoding_NoBOM(ref _enc);

            Stream _return = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_return, _enc);

            using (JsonTextWriter _jtw = new JsonTextWriter(_sw) { Formatting = Formatting.None })
            {
                JsonSerializer _js = new JsonSerializer();
                _js.Serialize(_jtw, _obj);
                _sw.Flush();
            }

            _return.Seek(0, SeekOrigin.Begin);

            return _return;
        }

        public static Stream ToStream(string _string, Encoding _enc = null)
        {
            ReadOnly.DefaultEncoding_NoBOM(ref _enc);

            Stream _return = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_return, _enc);
            _sw.Write(_string);
            _sw.Flush();

            _return.Seek(0, SeekOrigin.Begin);

            return _return;
        }
    }
}
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace imL.Utility.Newtonsoft_Json
{
    public static class NewtonsoftHelper
    {
        public static Stream ToStream(object _obj)
        {
            Stream _return = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_return, new UTF8Encoding(false));
            using (JsonTextWriter _jtw = new JsonTextWriter(_sw) { Formatting = Formatting.None })
            {
                JsonSerializer _js = new JsonSerializer();
                _js.Serialize(_jtw, _obj);
                _sw.Flush();
            }

            return _return;
        }
    }
}
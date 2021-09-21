#if (NET35 || NET40) == false

using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace imL.Utility.Newtonsoft_Json
{
    public static class NewtonsoftHelper
    {
        public static Stream ToJsonToStream(object _obj)
        {
            Stream _return = new MemoryStream();

            using (StreamWriter _sw = new StreamWriter(_return, new UTF8Encoding(false), 128, true))
            {
                using (JsonTextWriter _jtw = new JsonTextWriter(_sw) { Formatting = Formatting.None })
                {
                    JsonSerializer _js = new JsonSerializer();
                    _js.Serialize(_jtw, _obj);
                    _sw.Flush();
                }
            }

            _return.Seek(0, SeekOrigin.Begin);

            return _return;
        }
    }
}

#endif
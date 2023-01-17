#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace imL
{
    public static class GenericNS2_0Extension
    {
        public static string GetXml<G>(this G _this, Encoding _enc = null)
        {
            if (_this == null)
                return null;

            ReadOnly.EncodingDefault(ref _enc);

            XmlWriterSettings _format = new XmlWriterSettings { Encoding = _enc };

#if (NET35 || NET40) == false
            _format.Async = true;
#endif

            if (_this is DataTable || _this is DataSet)
            {
                using (StringWriter _sw = new StringWriterWithEncoding(_enc))
                {
                    using (XmlWriter _w = XmlWriter.Create(_sw, _format))
                    {
                        if (_this is DataTable _dt)
                            _dt.WriteXml(_w);
                        if (_this is DataSet _ds)
                            _ds.WriteXml(_w);
                    }

                    return _sw.ToString();
                }
            }
            else
            {
                XmlSerializer _xml = new XmlSerializer(_this.GetType());

                using (StringWriter _sw = new StringWriterWithEncoding(_enc))
                {
                    using (XmlWriter _w = XmlWriter.Create(_sw, _format))
                        _xml.Serialize(_w, _this);

                    return _sw.ToString();
                }
            }
        }

        public static void XmlCreate<G>(this G _this, string _path, Encoding _enc = null)
        {
            if (_this == null)
                return;

            ReadOnly.EncodingDefault(ref _enc);

            XmlWriterSettings _format = new XmlWriterSettings { Encoding = _enc };

#if (NET35 || NET40) == false
            _format.Async = true;
#endif

            if (_this is DataTable || _this is DataSet)
            {
                using (XmlWriter _w = XmlWriter.Create(_path, _format))
                {
                    if (_this is DataTable _dt)
                        _dt.WriteXml(_w);
                    if (_this is DataSet _ds)
                        _ds.WriteXml(_w);
                }
            }
            else
            {
                XmlSerializer _xml = new XmlSerializer(_this.GetType());

                using (XmlWriter _w = XmlWriter.Create(_path, _format))
                    _xml.Serialize(_w, _this);
            }
        }
    }
}

#endif
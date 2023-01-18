#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

#if (NET40_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Xml;
using System.Xml.Linq;
#endif

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace imL
{
    public static class DataTableExtension
    {
        public static void FlatCreate(this DataTable _this,
            string _path,
            char _separator = '\0',
            bool _columnnames = true,
            Encoding _enc = null,
            IProgress<int> _progress = null)
        {
            _this.GetFlat(out Stream _out, _separator, _columnnames, _enc, _progress);
            _out.CheckBeginPosition();
            _out.FileCreate(_path);
            _out.Dispose();
        }

        public static void GetFlat(this DataTable _this,
            out Stream _out,
            char _separator = '\0',
            bool _columnnames = true,
            Encoding _enc = null,
            IProgress<int> _progress = null)
        {
            ReadOnly.DefaultEncoding(ref _enc);

            string _sep = Convert.ToString(_separator);

            _out = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_out, _enc);
            bool _fw = false;

            if (_columnnames)
            {
                IList<string> _line = new List<string>();

                foreach (DataColumn _item in _this.Columns)
                    _line.Add(_item.Caption ?? _item.ColumnName);

#if (NET35)
                _sw.Write(string.Join(_sep, _line.ToArray()));
#else
                _sw.Write(string.Join(_sep, _line));
#endif

                _fw = true;
            }

            for (int _i = 0; _i < _this.Rows.Count; _i++)
            {
                DataRow _item = _this.Rows[_i];

                if (_fw)
                    _sw.WriteLine();

                _sw.Write(string.Join(_sep, _item.ItemArray.DBToString()));

                if (_fw == false)
                    _fw = true;

                _progress?.Report(_i);
            }

            _sw.Flush();
        }

#if (NET35) == false
        public static IEnumerable<XmlElement> ToXmlElements(this DataTable _dt)
        {
            if (_dt == null)
                throw new ArgumentNullException(nameof(_dt));

            IList<XmlElement> _return = new List<XmlElement>();
            XmlElement _tmp;

            using (MemoryStream _ms = new MemoryStream())
            {
                using (XmlWriter _w = XmlWriter.Create(_ms))
                    _dt.WriteXmlSchema(_w);

                _ms.Position = 0;
                _tmp = XElement.Load(_ms).ToXmlElement();

                if (_tmp != null)
                    _return.Add(_tmp);
            }

            using (MemoryStream _ms = new MemoryStream())
            {
                using (XmlWriter _w = XmlWriter.Create(_ms))
                    _dt.WriteXml(_w, XmlWriteMode.DiffGram);

                _ms.Position = 0;
                _tmp = XElement.Load(_ms).ToXmlElement();

                if (_tmp != null)
                    _return.Add(_tmp);
            }

            return _return;
        }
#endif

    }
}

#endif
﻿#if (NET40_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace imL.Utility
{
    public static class DataSetExtension
    {

#if (NET35) == false
        public static XmlElement[] ToXmlElements(DataSet _ds)
        {
            if (_ds == null)
                throw new ArgumentNullException(nameof(_ds));

            List<XmlElement> _return = new List<XmlElement>();
            XmlElement _tmp;

            using (MemoryStream _ms = new MemoryStream())
            {
                using (XmlWriter _w = XmlWriter.Create(_ms))
                    _ds.WriteXmlSchema(_w);

                _ms.Position = 0;
                _tmp = XElement.Load(_ms).ToXmlElement();

                if (_tmp != null)
                    _return.Add(_tmp);
            }

            using (MemoryStream _ms = new MemoryStream())
            {
                using (XmlWriter _w = XmlWriter.Create(_ms))
                    _ds.WriteXml(_w, XmlWriteMode.DiffGram);

                _ms.Position = 0;
                _tmp = XElement.Load(_ms).ToXmlElement();

                if (_tmp != null)
                    _return.Add(_tmp);
            }

            return _return.ToArray();
        }
#endif

    }
}

#endif
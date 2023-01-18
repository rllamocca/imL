#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Xml;
using System.Xml.Linq;

namespace imL
{
    public static class XElementExtension
    {
        public static XmlElement ToXmlElement(this XElement _this)
        {
            XmlDocument _doc = new XmlDocument();
            _doc.Load(_this.CreateReader());

            return _doc.DocumentElement;
        }
    }
}

#endif
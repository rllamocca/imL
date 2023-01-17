using System.IO;
using System.Text;

namespace imL
{
    public sealed class StringWriterWithEncoding : StringWriter
    {
        readonly Encoding _ENCODING;

        public StringWriterWithEncoding(Encoding _enc = null)
        {
            ReadOnly.EncodingDefault(ref _enc);

            this._ENCODING = _enc;
        }

        public override Encoding Encoding
        {
            get { return this._ENCODING; }
        }
    }
}

#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;

namespace imL.Format
{
    public class StreamAttachmentFormat
    {
        public Stream Content { set; get; }
        public string Name { set; get; }
    }
}

#endif
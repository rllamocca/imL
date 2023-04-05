#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;

namespace imL
{
    public class StreamAttachmentFormat
    {
        public string Name { set; get; }
        public string MediaType { set; get; }
        public Stream Content { set; get; }
    }
}

#endif
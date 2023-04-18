#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;

namespace imL
{
    public record StreamAttachmentFormat
    {
        public string? Name { init; get; }
        public string? MediaType { init; get; }
        public Stream? Content { init; get; }
    }
}

#endif
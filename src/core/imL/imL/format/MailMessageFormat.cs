#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;
using System.Net.Mime;

namespace imL
{
    public record MailMessageFormat
    {
        public string? Encoding { init; get; }

        public string? Subject { init; get; }
        public MailPriority? Priority { init; get; }
        public bool? IsBodyHtml { init; get; }
        public TransferEncoding? BodyTransferEncoding { init; get; }
        public string? Body { init; get; }
        public DeliveryNotificationOptions? DeliveryNotificationOptions { init; get; }


        public string? FromAddress { init; get; }
        public string? FromDisplayName { init; get; }

        public string?[]? TO { init; get; }
        public string?[]? CC { init; get; }
        public string?[]? BCC { init; get; }
        public string?[]? PathAttachments { init; get; }
        public StreamAttachmentFormat?[]? StreamAttachments { init; get; }
    }
}

#endif
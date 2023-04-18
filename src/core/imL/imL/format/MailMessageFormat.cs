#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Collections.Generic;
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

        public IEnumerable<string>? TO { init; get; }
        public IEnumerable<string>? CC { init; get; }
        public IEnumerable<string>? BCC { init; get; }
        public IEnumerable<string>? PathAttachments { init; get; }
        public IEnumerable<StreamAttachmentFormat>? StreamAttachments { init; get; }
    }
}

#endif
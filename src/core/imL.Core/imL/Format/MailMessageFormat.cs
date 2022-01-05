#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace imL.Format
{
    public class MailMessageFormat
    {
        public string Subject { set; get; }
        public MailPriority Priority { set; get; }
        public bool? IsBodyHtml { set; get; }
        public TransferEncoding BodyTransferEncoding { set; get; }
        public string Body { set; get; }
        public DeliveryNotificationOptions DeliveryNotificationOptions { set; get; }


        public string FromAddress { set; get; }
        public string FromDisplayName { set; get; }

        public IEnumerable<string> TO { set; get; }
        public IEnumerable<string> CC { set; get; }
        public IEnumerable<string> BCC { set; get; }
        public IEnumerable<string> PathAttachments { set; get; }
        public IEnumerable<StreamAttachment> StreamAttachments { set; get; }
    }

    public class StreamAttachment
    {
        public Stream Content { set; get; }
        public string Name { set; get; }
    }
}

#endif
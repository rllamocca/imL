#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using imL.Format;

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace imL.Utility
{
    public static class SmtpHelper
    {
        internal static void Init_SmtpClient(ref SmtpClient _ref, SmtpFormat _format)
        {
            _ref.Timeout = _format.Timeout ?? _ref.Timeout;
            _ref.TargetName = _format.TargetName ?? _ref.TargetName;
            _ref.Port = _format.Port ?? _ref.Port;
            _ref.PickupDirectoryLocation = _format.PickupDirectoryLocation ?? _ref.PickupDirectoryLocation;
            _ref.Host = _format.Host ?? _ref.Host;
            _ref.EnableSsl = _format.EnableSsl ?? _ref.EnableSsl;
            _ref.DeliveryMethod = _format.DeliveryMethod;

#if (NET35 || NET40) == false
            _ref.DeliveryFormat = _format.DeliveryFormat;
#endif

            _ref.UseDefaultCredentials = _format.UseDefaultCredentials ?? _ref.UseDefaultCredentials;

            if (_ref.UseDefaultCredentials == false)
                _ref.Credentials = new NetworkCredential(_format.UserName, _format.Password);
        }
        internal static void Init_MailMessage(ref MailMessage _ref, MailMessageFormat _format, Encoding _enc)
        {

#if (NET35) == false
            _ref.HeadersEncoding = _enc;
#endif

            _ref.SubjectEncoding = _enc;
            _ref.BodyEncoding = _enc;

            _ref.IsBodyHtml = _format.IsBodyHtml ?? _ref.IsBodyHtml;
            _ref.Priority = _format.Priority;

#if (NET35 || NET40) == false
            _ref.BodyTransferEncoding = _format.BodyTransferEncoding;
#endif

            _ref.DeliveryNotificationOptions = _format.DeliveryNotificationOptions;

            _ref.From = new MailAddress(_format.FromAddress, _format.FromDisplayName, _enc);
            _ref.Sender = _ref.From;

            foreach (string _item2 in _format.TO.DefaultOrEmpty())
                _ref.To.Add(new MailAddress(_item2));

            foreach (string _item2 in _format.CC.DefaultOrEmpty())
                _ref.CC.Add(new MailAddress(_item2));

            foreach (string _item2 in _format.BCC.DefaultOrEmpty())
                _ref.Bcc.Add(new MailAddress(_item2));

            _ref.Subject = _format.Subject;

            foreach (string _item2 in _format.PathAttachments.DefaultOrEmpty())
                _ref.Attachments.Add(new Attachment(_item2));

            foreach (StreamAttachment _item2 in _format.StreamAttachments.DefaultOrEmpty())
                _ref.Attachments.Add(new Attachment(_item2.Content, _item2.Name));

            _ref.Body = _format.Body;

            if (_ref.IsBodyHtml)
            {
                ContentType _ct = new ContentType("text/html");
                AlternateView _aw = AlternateView.CreateAlternateViewFromString(_ref.Body, _ct);
                _ref.AlternateViews.Add(_aw);
            }
        }

        public static void Send(SmtpFormat _smtp, Encoding _enc = null, params MailMessageFormat[] _messages)
        {
            if (_smtp == null) return;
            if (_messages.IsEmpty()) return;

            ReadOnly.DefaultEncoding(ref _enc);

            SmtpClient _client = new SmtpClient();
            SmtpHelper.Init_SmtpClient(ref _client, _smtp);

            for (int _i = 0; _i < _messages.Length; _i++)
            {
                MailMessage _mm = new MailMessage();
                SmtpHelper.Init_MailMessage(ref _mm, _messages[_i], _enc);
                _client.Send(_mm);
                _mm.Dispose();
            }

#if (NET35) == false
            _client.Dispose();
#endif

        }
    }
}

#endif
#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace imL
{
    public static class SmtpHelper
    {
        internal static SmtpClient InitSmtpClient(SmtpClient _i, SmtpFormat _f)
        {
            _i.Timeout = _f.Timeout ?? _i.Timeout;
            _i.TargetName = _f.TargetName ?? _i.TargetName;
            _i.Port = _f.Port ?? _i.Port;
            _i.PickupDirectoryLocation = _f.PickupDirectoryLocation ?? _i.PickupDirectoryLocation;
            _i.Host = _f.Host ?? _i.Host;
            _i.EnableSsl = _f.EnableSsl ?? _i.EnableSsl;
            _i.DeliveryMethod = _f.DeliveryMethod ?? _i.DeliveryMethod;

#if (NET35 || NET40) == false
            _i.DeliveryFormat = _f.DeliveryFormat ?? _i.DeliveryFormat;
#endif

            _i.UseDefaultCredentials = _f.UseDefaultCredentials ?? _i.UseDefaultCredentials;

            if (_i.UseDefaultCredentials != true)
                _i.Credentials = new NetworkCredential(_f.UserName, _f.Password);

            return _i;
        }
        internal static MailMessage InitMailMessage(MailMessage _i, MailMessageFormat _f)
        {
            Encoding _enc = _f.Encoding == null ? null : Encoding.GetEncoding(_f.Encoding);

#if (NET35) == false
            _i.HeadersEncoding = _enc ?? _i.HeadersEncoding;
#endif

            _i.SubjectEncoding = _enc ?? _i.SubjectEncoding;
            _i.BodyEncoding = _enc ?? _i.BodyEncoding;

            _i.IsBodyHtml = _f.IsBodyHtml ?? _i.IsBodyHtml;
            _i.Priority = _f.Priority ?? _i.Priority;

#if (NET35 || NET40) == false
            _i.BodyTransferEncoding = _f.BodyTransferEncoding ?? _i.BodyTransferEncoding;
#endif

            _i.DeliveryNotificationOptions = _f.DeliveryNotificationOptions ?? _i.DeliveryNotificationOptions;

            _i.From = new MailAddress(_f.FromAddress, _f.FromDisplayName, _enc);
            _i.Sender = _i.From;

            _i.Body = _f.Body ?? _i.Body;

            if (_i.IsBodyHtml)
            {
                AlternateView _aw = AlternateView.CreateAlternateViewFromString(_i.Body, new ContentType("text/html"));
                _i.AlternateViews.Add(_aw);
            }

            foreach (string _item in _f.TO.DefaultOrEmpty())
                _i.To.Add(new MailAddress(_item));

            foreach (string _item in _f.CC.DefaultOrEmpty())
                _i.CC.Add(new MailAddress(_item));

            foreach (string _item in _f.BCC.DefaultOrEmpty())
                _i.Bcc.Add(new MailAddress(_item));

            _i.Subject = _f.Subject;

            foreach (string _item in _f.PathAttachments.DefaultOrEmpty())
                _i.Attachments.Add(InitAttachment(_item));

            foreach (StreamAttachmentFormat _item in _f.StreamAttachments.DefaultOrEmpty())
                _i.Attachments.Add(new Attachment(_item.Content, _item.Name, _item.MediaType ?? MimeHelper.MediaType(new FileInfo(_item.Name).Extension)));

            return _i;
        }
        internal static Attachment InitAttachment(string _path)
        {
            Attachment _return = new Attachment(_path, MimeHelper.ContentType(_path));
            FileInfo _fi = new FileInfo(_path);

            ContentDisposition _d = _return.ContentDisposition;
            _d.CreationDate = _fi.CreationTime;
            _d.ModificationDate = _fi.LastWriteTime;
            _d.ReadDate = _fi.LastAccessTime;
            _d.FileName = _fi.Name;
            _d.Size = _fi.Length;
            _d.DispositionType = DispositionTypeNames.Attachment;

            return _return;
        }

        public static void Send(SmtpFormat _smtp, params MailMessageFormat[] _messages)
        {
            if (_smtp == null) return;
            if (_messages.IsEmpty()) return;

            SmtpClient _client = InitSmtpClient(new SmtpClient(), _smtp);

            foreach (MailMessageFormat _item in _messages)
            {
                _item.FromAddress = _item.FromAddress ?? _smtp.UserName;
                _item.FromDisplayName = _item.FromDisplayName ?? _smtp.UserName;

                using (MailMessage _mm = InitMailMessage(new MailMessage(), _item))
                    _client.Send(_mm);
            }

#if (NET35) == false
            _client.Dispose();
#endif

        }
    }
}

#endif
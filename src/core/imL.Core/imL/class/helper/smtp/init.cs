#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace imL
{
    public static partial class SmtpHelper
    {
        internal static SmtpClient InitSmtpClient(SmtpClient _i, SmtpFormat _f)
        {
            _i.Timeout = _f.Timeout ?? _i.Timeout;
            _i.TargetName = _f.TargetName ?? _i.TargetName;
            _i.Port = _f.Port ?? _i.Port;
            _i.PickupDirectoryLocation = _f.PickupDirectoryLocation ?? _i.PickupDirectoryLocation;

            string? _host = _f.Host ?? _i.Host;

            //[DisallowNull]
            if (_host != null)
                _i.Host = _host;

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
        internal static MailMessage InitMailMessage(MailMessage _i, SmtpFormat _f, MailMessageFormat _f1)
        {
            string? _FromAddress = _f1.FromAddress ?? _f.UserName;
            string? _FromDisplayName = _f1.FromDisplayName ?? _f.UserName;
            Encoding? _enc = _f1.Encoding == null ? null : Encoding.GetEncoding(_f1.Encoding);

            _FromAddress ??= string.Empty;
            _FromDisplayName ??= string.Empty;

#if (NET35) == false
            _i.HeadersEncoding = _enc ?? _i.HeadersEncoding;
#endif

            _i.SubjectEncoding = _enc ?? _i.SubjectEncoding;
            _i.BodyEncoding = _enc ?? _i.BodyEncoding;

            _i.IsBodyHtml = _f1.IsBodyHtml ?? _i.IsBodyHtml;
            _i.Priority = _f1.Priority ?? _i.Priority;

#if (NET35 || NET40) == false
            _i.BodyTransferEncoding = _f1.BodyTransferEncoding ?? _i.BodyTransferEncoding;
#endif

            _i.DeliveryNotificationOptions = _f1.DeliveryNotificationOptions ?? _i.DeliveryNotificationOptions;

            _i.From = new MailAddress(_FromAddress, _FromDisplayName, _enc);
            _i.Sender = _i.From;

            _i.Body = _f1.Body ?? _i.Body;

            if (_i.IsBodyHtml)
            {
                AlternateView _aw = AlternateView.CreateAlternateViewFromString(_i.Body, new ContentType("text/html"));
                _i.AlternateViews.Add(_aw);
            }

            if (_f1.TO != null && _f1.TO.Any())
                foreach (string? _item in _f1.TO)
                    _i.To.Add(new MailAddress(_item));

            if (_f1.CC != null && _f1.CC.Any())
                foreach (string? _item in _f1.CC)
                    _i.CC.Add(new MailAddress(_item));

            if (_f1.BCC != null && _f1.BCC.Any())
                foreach (string? _item in _f1.BCC)
                    _i.Bcc.Add(new MailAddress(_item));

            _i.Subject = _f1.Subject;

            if (_f1.PathAttachments != null && _f1.PathAttachments.Any())
                foreach (string? _item in _f1.PathAttachments)
                    _i.Attachments.Add(InitAttachment(_item));

            if (_f1.StreamAttachments != null && _f1.StreamAttachments.Any())
                foreach (StreamAttachmentFormat _item in _f1.StreamAttachments)
                {
                    if (_item.Content == null)
                        continue;

                    if (_item.Name == null)
                        _i.Attachments.Add(new Attachment(_item.Content, _item.Name, _item.MediaType));
                    else
                        _i.Attachments.Add(new Attachment(_item.Content, _item.Name, _item.MediaType ?? MimeHelper.MediaType(new FileInfo(_item.Name).Extension)));
                }

            return _i;
        }
        internal static Attachment InitAttachment(string _path)
        {
            Attachment _return = new(_path, MimeHelper.ContentType(_path));

            if (_return.ContentDisposition == null)
                return _return;

            FileInfo _fi = new(_path);

            if (_fi.Exists == false)
                return _return;

            ContentDisposition _cd = _return.ContentDisposition;
            _cd.CreationDate = _fi.CreationTime;
            _cd.ModificationDate = _fi.LastWriteTime;
            _cd.ReadDate = _fi.LastAccessTime;
            _cd.FileName = _fi.Name;
            _cd.Size = _fi.Length;
            _cd.DispositionType = DispositionTypeNames.Attachment;

            return _return;
        }
    }
}

#endif
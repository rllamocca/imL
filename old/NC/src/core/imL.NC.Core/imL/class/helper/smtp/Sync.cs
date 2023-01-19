#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;

namespace imL
{
    public static partial class SmtpHelper
    {
        public static void Send(SmtpFormat _smtp, params MailMessageFormat[] _messages)
        {
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
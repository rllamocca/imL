#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;

namespace imL
{
    public static partial class SmtpHelper
    {
        public static void Send(SmtpRecord _smtp, params MailMessageRecord[] _params)
        {
            SmtpClient _client = InitSmtpClient(new SmtpClient(), _smtp);

            foreach (MailMessageRecord _item in _params)
            {
                using MailMessage _mm = InitMailMessage(new MailMessage(), _smtp, _item);
                _client.Send(_mm);
            }

#if (NET35) == false
            _client.Dispose();
#endif

        }
    }
}

#endif
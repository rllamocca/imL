#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace imL
{
    public static partial class SmtpHelper
    {
        public static async Task SendAsync(SmtpFormat _smtp, CancellationToken _ct = default, params MailMessageFormat[] _messages)
        {
            SmtpClient _client = InitSmtpClient(new SmtpClient(), _smtp);

            foreach (MailMessageFormat _item in _messages)
            {
                _item.FromAddress = _item.FromAddress ?? _smtp.UserName;
                _item.FromDisplayName = _item.FromDisplayName ?? _smtp.UserName;

                using (MailMessage _mm = InitMailMessage(new MailMessage(), _item))
                    await _client.SendMailAsync(_mm, _ct);
            }

            _client.Dispose();
        }
    }
}
#endif
#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace imL
{
    public static partial class SmtpHelper
    {
        public static async Task SendAsync(SmtpRecord _smtp, CancellationToken _ct = default, params MailMessageRecord[] _params)
        {
            SmtpClient _client = InitSmtpClient(new SmtpClient(), _smtp);

            foreach (MailMessageRecord _item in _params)
            {
                using (MailMessage _mm = InitMailMessage(new MailMessage(), _smtp, _item))
#if (NETFRAMEWORK || NETSTANDARD)
                    await _client.SendMailAsync(_mm);
#else
                    await _client.SendMailAsync(_mm, _ct);
#endif
            }

            _client.Dispose();
        }
    }
}
#endif
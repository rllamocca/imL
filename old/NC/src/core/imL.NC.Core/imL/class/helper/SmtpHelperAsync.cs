#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;

namespace imL
{
    public static class SmtpHelperAsync
    {
        public static async Task SendAsync(SmtpFormat _smtp, params MailMessageFormat[] _messages)
        {
            if (_smtp == null) return;
            if (_messages.IsEmpty()) return;

            SmtpClient _client = SmtpHelper.InitSmtpClient(new SmtpClient(), _smtp);

            foreach (MailMessageFormat _item in _messages)
            {
                _item.FromAddress = _item.FromAddress ?? _smtp.UserName;
                _item.FromDisplayName = _item.FromDisplayName ?? _smtp.UserName;

                using (MailMessage _mm = SmtpHelper.InitMailMessage(new MailMessage(), _item))
                    await _client.SendMailAsync(_mm);
            }

            _client.Dispose();
        }
    }
}
#endif
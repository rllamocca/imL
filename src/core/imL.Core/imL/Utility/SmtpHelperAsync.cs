#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using imL.Format;

namespace imL.Utility
{
    public static class SmtpHelperAsync
    {
        public static async Task SendAsync(SmtpFormat _smtp, Encoding _enc = null, params MailMessageFormat[] _messages)
        {
            if (_smtp == null) return;
            if (_messages.IsEmpty()) return;

            ReadOnly.DefaultEncoding(ref _enc);

            SmtpClient _client = new SmtpClient();
            SmtpHelper.Init_SmtpClient(ref _client, _smtp);

            for (int _i = 0; _i < _messages.Length; _i++)
            {
                if (_messages[_i].FromAddress == null) _messages[_i].FromAddress = _smtp.UserName;
                if (_messages[_i].FromDisplayName == null) _messages[_i].FromDisplayName = _smtp.UserName;

                MailMessage _mm = new MailMessage();
                SmtpHelper.Init_MailMessage(ref _mm, _messages[_i], _enc);
                await _client.SendMailAsync(_mm);
                _mm.Dispose();
            }

            _client.Dispose();
        }
    }
}
#endif
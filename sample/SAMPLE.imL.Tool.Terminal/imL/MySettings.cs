using imL.Format;
using imL.Tool.Terminal;

namespace SAMPLE.imL.Tool.Terminal
{
    internal class MySettings : ISettings
    {
        public SmtpFormat Smtp { set; get; }
        public MailMessageFormat Mail { set; get; }
    }
}
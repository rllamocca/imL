using imL.Format;

namespace imL.Tool.Terminal
{
    public class SettingsDefault : ISettings
    {
        public SmtpFormat Smtp { set; get; }
        public MailMessageFormat Mail { set; get; }
    }
}

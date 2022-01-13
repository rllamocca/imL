using imL.Format;

namespace imL.Tool.Terminal
{
    public class SettingDefault : ISetting
    {
        public SmtpFormat Smtp { set; get; }
        public MailMessageFormat Mail { set; get; }
    }
}

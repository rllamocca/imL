using imL.Format;

namespace imL.Tool.Terminal
{
    public interface ISettings
    {
        SmtpFormat Smtp { set; get; }
        MailMessageFormat Mail { set; get; }
    }
}

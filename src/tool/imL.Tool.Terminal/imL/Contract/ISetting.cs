using imL.Format;

namespace imL.Tool.Terminal
{
    public interface ISetting
    {
        SmtpFormat Smtp { set; get; }
        MailMessageFormat Mail { set; get; }
    }
}

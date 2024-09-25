using imL;

namespace EmailDelivery;

public class SettingFormat
{
    public int? Selected { set; get; }
    public string? SplitSeparator { set; get; }

    public MailMessageFormat? MailMessageFormatBasic { set; get; }

    public SmtpFormat? SmtpFormatBasic { set; get; }
}

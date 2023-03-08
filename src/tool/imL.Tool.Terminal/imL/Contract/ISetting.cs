namespace imL.Tool.Terminal
{
    public interface ISetting
    {
        SmtpFormat Smtp { set; get; }
        MailMessageFormat Mail { set; get; }

        bool? OneZipFile { set; get; }
        long? MinSizeZipAttachment { set; get; } //MB
    }
}

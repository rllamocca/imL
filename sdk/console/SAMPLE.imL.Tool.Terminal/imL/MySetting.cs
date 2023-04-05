using imL.Tool.Terminal;
using imL;

namespace SAMPLE.imL.Tool.Terminal
{
    internal class MySetting : ISetting
    {
        public SmtpFormat Smtp { set; get; }
        public MailMessageFormat Mail { set; get; }

        public bool? OneZipFile { set; get; }
        public long? MinSizeZipAttachment { set; get; }
    }
}
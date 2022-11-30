﻿using imL.Format;
using imL.Tool.Terminal;

namespace SAMPLE.imL.Tool.Terminal
{
    internal class MySettings : ISetting
    {
        public SmtpFormat Smtp { set; get; }
        public MailMessageFormat Mail { set; get; }

        public bool? OneZipFile { set; get; }
        public long? MinSizeZipAttachment { set; get; }
    }
}
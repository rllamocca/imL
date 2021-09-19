namespace imL.Utility.Ftp
{
    public class FtpFormat
    {
        public bool? UseBinary { set; get; }
        public int? Timeout { set; get; }
        public int? ReadWriteTimeout { set; get; }
        public bool? KeepAlive { set; get; }
        public bool? EnableSsl { set; get; }
        public bool? UsePassive { set; get; }
        public bool? UseDefaultCredentials { set; get; }

        public int? Port { set; get; }

        public string Host { set; get; }
        public string Path { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}

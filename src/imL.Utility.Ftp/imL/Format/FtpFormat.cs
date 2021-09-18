namespace imL.Utility.Ftp
{
    public class FtpFormat
    {
        public bool? UseBinary { get; set; }
        public int? Timeout { get; set; }
        public int? ReadWriteTimeout { get; set; }
        public bool? KeepAlive { get; set; }
        public bool? EnableSsl { get; set; }
        public bool? UsePassive { get; set; }
        public bool? UseDefaultCredentials { set; get; }

        public int? Port { set; get; }

        public string Host { set; get; }
        public string Path { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}

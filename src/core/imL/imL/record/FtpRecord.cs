namespace imL
{
    public record FtpRecord
    {
        public bool? UseBinary { init; get; }
        public int? Timeout { init; get; }
        public int? ReadWriteTimeout { init; get; }
        public bool? KeepAlive { init; get; }
        public bool? EnableSsl { init; get; }
        public bool? UsePassive { init; get; }
        public bool? UseDefaultCredentials { init; get; }

        public string? ConnectionGroupName { init; get; }
        public int? ConnectionLimit { init; get; }

        public int? Port { init; get; }

        public string? Host { init; get; }
        public string? Path { init; get; }
        public string? UserName { init; get; }
        public string? Password { init; get; }
    }
}

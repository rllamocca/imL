namespace imL
{
    public record FtpContentFormat
    {
        public string? FullName { init; get; }
        public string? Name { init; get; }
        public long? Size { init; get; }
        public bool? IsDirectory { init; get; }
    }
}

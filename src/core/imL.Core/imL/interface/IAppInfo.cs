namespace imL
{
    public interface IAppInfo
    {
        string[] Args { get; }
        string Path { get; }
        string PathIn { get; }
        string PathOut { get; }
        string PathLog { get; }
        string PathTmp { get; }
        bool? InContainer { get; }
        bool? InTempPath { get; }
    }
}
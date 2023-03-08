namespace imL
{
    public interface IAppInfo
    {
        string[] Args { get; }
        string Base { get; }
        string BaseIn { get; }
        string BaseExe { get; }
        string BaseTmp { get; }

        bool? InContainer { get; }
        bool? InTempPath { get; }
    }
}
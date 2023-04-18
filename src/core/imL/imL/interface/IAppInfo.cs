namespace imL
{
    public interface IAppInfo
    {
#pragma warning disable IDE1006 // Estilos de nombres
        string[] args { get; }
#pragma warning restore IDE1006 // Estilos de nombres
        string Base { get; }
        string BaseIn { get; }
        string BaseExe { get; }
        string BaseTmp { get; }

        bool? InContainer { get; }
        bool? InTempPath { get; }
    }
}
using imL.Contract.DB;

namespace imL.Package.EFCSql
{
    public interface IContext : IDisposable, IAsyncDisposable
    {
        IConnection Connection { get; }
        IHelperAsync Helper { get; }
    }
}
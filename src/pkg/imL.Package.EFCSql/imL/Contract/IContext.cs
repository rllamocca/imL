using imL.DB;

namespace imL.Package.EFCSql
{
    public interface IContext : IDisposable, IAsyncDisposable
    {
        IConnection? Connection { get; }
        IHelper? Helper { get; }

        DateTime CURRENT_TIMESTAMP();
        DateTime GETDATE();
        decimal? ABS(decimal? numeric_expression);
    }
}
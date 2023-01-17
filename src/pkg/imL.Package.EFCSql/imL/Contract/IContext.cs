using imL.Contract.DB;

namespace imL.Package.EFCSql
{
    public interface IContext : IDisposable, IAsyncDisposable
    {
        IConnection? Connection { get; }
        IHelperAsync? Helper { get; }

        DateTime CURRENT_TIMESTAMP();
        DateTime GETDATE();
        decimal? ABS(decimal? numeric_expression);
    }
}
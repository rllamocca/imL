using System;
using System.Data;
using System.Threading.Tasks;

using imL.NC;
using imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer;

using Microsoft.Data.SqlClient;

namespace TRIAL.MicrosoftEntityFrameworkCoreSqlServer;

internal partial class Program
{
    static async Task AsyncTrial1(SqlConnection _c)
    {
        string _query = @"
SELECT GETDATE();
";

        Return _r = await _c.TryExecuteAsync(new CommandInput() { Execute = EExecute.Scalar, Query = _query });
        _r.TriggerErrorException();

        if (_r.Result == null)
            throw new ArgumentNullException(nameof(_r.Result));

        DateTime _result = (DateTime)_r.Result;

        Console.WriteLine(_result);
    }
    static async Task AsyncTrial2(SqlConnection _c)
    {
        string _query = @"
SELECT GETDATE(), @_A, @_B;
";

        DataTable? _dt = await _c.LoadDataTableAsync(new CommandInput()
        {
            Query = _query,
            Parameters = [
                new SqlParameter("@_A", 1000),
                new SqlParameter("@_B", null)
            ]
        });

        Resume(_dt);
    }
    static async Task AsyncTrial3(SqlConnection _c)
    {
        string _query = @"
SELECT TOP 1000 * FROM DE01_LIQUIDACION;
SELECT TOP 2000 * FROM DE02_DETALLELIQUIDACION;
SELECT TOP 3000 * FROM DE03_REMESA;
SELECT TOP 4000 * FROM DE04_LOTE;
SELECT TOP 5000 * FROM DE05_RECHAZOSLIQUIDACION;
";

        DataSet? _ds = await _c.LoadDataSetAsync(new CommandInput() { Query = _query });

        Resume(_ds);
    }
    static async Task AsyncTrial4(SqlConnection _c)
    {
        string _query = @"
SELECT * FROM DE01_LIQUIDACION WHERE EFOLDERKEY BETWEEN @_A AND @_B;
SELECT * FROM DE02_DETALLELIQUIDACION WHERE EFOLDERKEY BETWEEN @_A AND @_B;
";

        DataSet? _ds = await _c.LoadDataSetAsync(new CommandInput()
        {
            Query = _query,
            Parameters = [
                new SqlParameter("@_A", 1000),
                new SqlParameter("@_B", 2000)
            ]
        });

        Resume(_ds);
    }
    static async Task AsyncTrial5(SqlConnection _c)
    {
        string _query = @"
SELECT * FROM DE01_LIQUIDACION WHERE CREATEDDATE BETWEEN @_A AND @_B;
SELECT * FROM DE02_DETALLELIQUIDACION WHERE CREATEDDATE BETWEEN @_A AND @_B;
";

        DataSet? _ds = await _c.LoadDataSetAsync(new CommandInput()
        {
            Query = _query,
            Parameters = [
                new SqlParameter("@_A", new DateTime(2024, 01, 01)),
                new SqlParameter("@_B", new DateTime(2024, 01, 02))
            ]
        });

        Resume(_ds);
    }
}

using System;
using System.Data;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace TRIAL.MicrosoftEntityFrameworkCoreSqlServer;

internal partial class Program
{
    static async Task Main()
    {
        Console.WriteLine("Hello, World!");

        string _c = @"
Data Source=MX-SQL05.microexpertos.local;
Initial Catalog=VIDACAMARA-EFOLDER-QA;
User ID=VCAMARA-EFOLDER;
Password=Vcamara@1002;
MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=true;";

        try
        {
            Console.WriteLine("Open");
            using SqlConnection _using = new(_c);
            _using.Open();

            SyncTrial1(_using);
            SyncTrial2(_using);
            SyncTrial3(_using);
            SyncTrial4(_using);
            SyncTrial5(_using);
        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }
        try
        {
            Console.WriteLine("OpenAsync");
            using SqlConnection _using = new(_c);
            await _using.OpenAsync();

            await AsyncTrial1(_using);
            await AsyncTrial2(_using);
            await AsyncTrial3(_using);
            await AsyncTrial4(_using);
            await AsyncTrial5(_using);
        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        Console.WriteLine("Bye, World!");
    }

    static void Resume(DataSet? _ds)
    {
        if (_ds == null)
            throw new ArgumentNullException(nameof(_ds));

        Console.WriteLine("{0}: {1}", _ds.DataSetName, _ds.Tables.Count);

        foreach (DataTable _item in _ds.Tables)
            Console.WriteLine("\t{0}: {1}", _item.TableName, _item.Rows.Count);
    }
    static void Resume(DataTable? _dt)
    {
        if (_dt == null)
            throw new ArgumentNullException(nameof(_dt));

        Console.WriteLine("{0}: {1}", _dt.TableName, _dt.Rows.Count);
    }
}

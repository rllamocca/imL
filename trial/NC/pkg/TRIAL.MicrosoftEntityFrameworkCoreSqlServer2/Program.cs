using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using imL.NC;
using imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer;

using Microsoft.Data.SqlClient;
using System.Linq;

namespace TRIAL.MicrosoftEntityFrameworkCoreSqlServer2;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

#if DEBUG
        foreach (var _item in args)
            Console.WriteLine(_item);

        return;
#endif

        try
        {
            int[] _planes = (
                from _r in args
                select Convert.ToInt32(_r)
                )
                .Distinct()
                .ToArray();

            string _c;
#if DEBUG
            _c = @"
";
#else
            _c = @"
";
#endif

            Console.WriteLine("OpenAsync");
            using SqlConnection _using = new(_c);
            await _using.OpenAsync();

            DataSet? _ds = await _using.LoadDataSetAsync(new CommandInput() { Query = _QUERY_BASE });
            if (_ds == null)
                throw new ArgumentNullException(nameof(_ds));

            int _counter = 0;
            int _length = _planes.Length;

            foreach (int _item in _planes)
            {
                _counter++;

                try
                {
                    Console.WriteLine("{0} /{1}", _counter, _length);
                    Console.WriteLine("Plan: {0}", _item);

                    Return _r = await _using.TryExecuteAsync(new CommandInput()
                    {
                        Execute = EExecute.NonQuery,
                        Query = _QUERY_UPDATE,
                        Parameters = [
                            new SqlParameter("@_PLAN", _item)
                            ]
                    });
                    _r.TriggerErrorException();

                    for (int _i = 0; _i < 14; _i++)
                    {
                        string _nombre = _NOMBRES[_i];
                        byte _tiempo = _TIEMPOS[_i];
                        byte _cantidad = _CANTIDADES[_i];
                        byte _tabla = _TABLAS[_i];

                        _r = await _using.TryExecuteAsync(new CommandInput()
                        {
                            Execute = EExecute.Scalar,
                            Query = _QUERY_MN57,
                            Parameters = [
                                new SqlParameter("@MN57_NOMBRE", _nombre)
                                ,new SqlParameter("@MN57_CANTIDAD", _cantidad)
                                ,new SqlParameter("@MN57_PLAN", _item)
                                ,new SqlParameter("@MN57_TIEMPO", _tiempo)
                                ]
                        });
                        _r.TriggerErrorException();

                        if (_r.Result == null)
                            throw new ArgumentNullException(nameof(_r.Result));

                        decimal? _result = (decimal?)_r.Result;

                        Console.WriteLine("{0}: {1}", _nombre, _result);

                        List<SqlParameter[]> _ps = new List<SqlParameter[]>();

                        foreach (DataRow _item2 in _ds.Tables[_tabla].Rows)
                        {
                            SqlParameter _a = new SqlParameter("@MN58_FONASA", SqlDbType.BigInt);
                            _a.Value = _item2[0];
                            SqlParameter _b = new SqlParameter("@MN58_TOPEIMED", SqlDbType.BigInt);
                            _b.Value = _result;

                            _ps.Add(
                                [
                                _a
                                ,_b
                                ]
                                );
                        }

                        Console.WriteLine("items: {0}", _ps.Count);

                        Return[] _rA = await _using.TryExecutesAsync(new CommandInputs()
                        {
                            Execute = EExecute.NonQuery,
                            Query = _QUERY_MN58,
                            Parameters = _ps.ToArray()
                        });
                        _rA.TriggerErrorException();
                    }
                }
                catch (Exception _ex)
                {
                    Console.WriteLine(_ex);
                }
            }
        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        Console.WriteLine("Goodbye, World!");
    }
}
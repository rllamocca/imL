using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer
{
    public static partial class SqlConnectionExtension
    {
        static async Task<Return> ExecuteAsync(SqlCommand _c, EExecute? _e, CancellationToken _ct = default)
        {
            switch (_e)
            {
                case EExecute.NonQuery:
                    return new Return(true, await _c.ExecuteNonQueryAsync(_ct));
                case EExecute.Scalar:
                    return new Return(true, await _c.ExecuteScalarAsync(_ct));
                case EExecute.Reader:
                    return new Return(true, await _c.ExecuteReaderAsync(_ct));
                case EExecute.XmlReader:
                    return new Return(true, await _c.ExecuteXmlReaderAsync(_ct));
                default:
                    return new Return(false);
            }
        }
        public static async Task<Return> TryExecuteAsync(this SqlConnection _this, CommandInput _input, CancellationToken _ct = default)
        {
            try
            {
                using SqlCommand _using = NEWSqlCommand(_this, _input);

                return await ExecuteAsync(_using, _input.Execute, _ct);
            }
            catch (Exception _ex)
            {
                return new Return(_ex);
            }
        }
        public static async Task<Return[]> TryExecutesAsync(this SqlConnection _this, CommandInputs _inputs, CancellationToken _ct = default)
        {
            try
            {
                using SqlCommand _using = NEWSqlCommand(_this, CommandInputs.NEWCommandInput(_inputs), true);

                if (_inputs.Parameters == null)
                    return new Return[] { await ExecuteAsync(_using, _inputs.Execute, _ct) };
                else
                    await _using.PrepareAsync(_ct);

                List<Return> _return = new List<Return>();

                foreach (SqlParameter[] _item in _inputs.Parameters)
                {
                    Return _add;

                    try
                    {
                        _using.OverwriteParameters(_item);
                        _add = await ExecuteAsync(_using, _inputs.Execute, _ct);
                    }
                    catch (Exception _ex)
                    {
                        _add = new Return(_ex);
                    }

                    _return.Add(_add);
                }

                return _return.ToArray();
            }
            catch (Exception _ex)
            {
                return new Return[] { new Return(_ex) };
            }
        }

        static async Task<Return?> LoadDataAsync(SqlConnection _this, CommandInput _input, CancellationToken _ct = default)
        {
            _input.Execute = EExecute.Reader;
            Return _return = await _this.TryExecuteAsync(_input, _ct);
            _return.TriggerErrorException();

            if (_return.Result == null)
                return null;

            return _return;
        }
        public static async Task<DataSet?> LoadDataSetAsync(this SqlConnection _this, CommandInput _input, CancellationToken _ct = default)
        {
            Return? _exe = await LoadDataAsync(_this, _input, _ct);

            if (_exe == null || _exe.Result == null)
                return null;

            using SqlDataReader _using = (SqlDataReader)_exe.Result;
            return LOADDataSet(_using, _input);
        }
        public static async Task<DataTable?> LoadDataTableAsync(this SqlConnection _this, CommandInput _input, CancellationToken _ct = default)
        {
            Return? _exe = await LoadDataAsync(_this, _input, _ct);

            if (_exe == null || _exe.Result == null)
                return null;

            using SqlDataReader _using = (SqlDataReader)_exe.Result;
            return LOADDataTable(_using);
        }
    }
}

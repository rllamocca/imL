using System;
using System.Data;

using Microsoft.Data.SqlClient;

namespace imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer
{
    public static partial class SqlConnectionExtension
    {
        static SqlCommand NEWSqlCommand(SqlConnection _connection, CommandInput _input, bool _prepare = false)
        {
            SqlCommand _return = new(_input.Query, _connection);

            SqlParameter[]? _ps;

            if (_prepare)
                _ps = _input.Parameters.Prepare();
            else
                _ps = _input.Parameters.OverwriteNULL();

            if (_ps != null)
                _return.Parameters.AddRange(_ps);

            _return.CommandTimeout = _input.Timeout ?? _return.CommandTimeout;
            _return.Transaction = _input.Transaction ?? _return.Transaction;

            return _return;
        }

        static DataSet? LOADDataSet(SqlDataReader _reader, CommandInput _input)
        {
            DataSet _return = new("DataSet_0") { EnforceConstraints = _input.EnforceConstraints ?? true };

            int _e = 0;
            while (_reader.IsClosed == false)
            {
                DataTable _add = new("DataTable_" + Convert.ToString(_e));
                _add.Load(_reader, LoadOption.OverwriteChanges);
                _return.Tables.Add(_add);

                _e++;
            }

            return _return;
        }
        static DataTable? LOADDataTable(SqlDataReader _reader)
        {
            DataTable _return = new("DataTable_0");
            _return.Load(_reader, LoadOption.OverwriteChanges);

            return _return;
        }
    }
}

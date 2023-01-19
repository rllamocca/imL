#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System;
using System.Data;
#endif


using System;
using System.Collections;
using System.Collections.Generic;

namespace imL.DB
{
    public partial interface IHelper
    {
        Return Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts);
        Return Execute(string _query, EExecute _exe = EExecute.NonQuery);
        IEnumerable<Return> Executions(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _pmts);
        IEnumerable<Return> Executions(string _query, EExecute _exe = EExecute.NonQuery);

#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        DataTable LoadDataTable(string _query, params IParameter[] _pmts);
        DataTable LoadDataTable(string _query);
        DataSet LoadDataSet(string _query, params IParameter[] _pmts);
        DataSet LoadDataSet(string _query);
        IEnumerable<G> LoadData<G>(string _query, params IParameter[] _pmts);
        IEnumerable<G> LoadData<G>(string _query);

#endif
    }
}

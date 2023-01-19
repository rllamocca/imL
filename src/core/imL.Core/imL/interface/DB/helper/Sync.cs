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
        Return Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _params);
        Return Execute(string _query, EExecute _exe = EExecute.NonQuery);
        IEnumerable<Return> Executions(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _params);
        IEnumerable<Return> Executions(string _query, EExecute _exe = EExecute.NonQuery);

#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        DataTable LoadDataTable(string _query, params IParameter[] _params);
        DataTable LoadDataTable(string _query);
        DataSet LoadDataSet(string _query, params IParameter[] _params);
        DataSet LoadDataSet(string _query);
        IEnumerable<G> LoadData<G>(string _query, params IParameter[] _params);
        IEnumerable<G> LoadData<G>(string _query);

#endif
    }
}

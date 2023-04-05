#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System;
using System.Data;
#endif


using System.Collections.Generic;

namespace imL.DB
{
    public partial interface IHelper
    {
        IAppReturn<object> Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _params);
        IAppReturn<object> Execute(string _query, EExecute _exe = EExecute.NonQuery);
        IEnumerable<IAppReturn<object>> Executions(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _params);
        IEnumerable<IAppReturn<object>> Executions(string _query, EExecute _exe = EExecute.NonQuery);

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

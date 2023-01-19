#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System;
using System.Data;
using System.Threading.Tasks;
#endif


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace imL.DB
{
    public partial interface IHelper
    {
        Task<Return> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default, params IParameter[] _pmts);
        Task<Return> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default);
        Task<IEnumerable<Return>> ExecutionsAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default, params IParameter[][] _pmts);
        Task<IEnumerable<Return>> ExecutionsAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default);

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        Task<DataTable> LoadDataTableAsync(string _query, CancellationToken _ct = default, params IParameter[] _pmts);
        Task<DataTable> LoadDataTableAsync(string _query, CancellationToken _ct = default);
        Task<DataSet> LoadDataSetAsync(string _query, CancellationToken _ct = default, params IParameter[] _pmts);
        Task<DataSet> LoadDataSetAsync(string _query, CancellationToken _ct = default);
        Task<IEnumerable<G>> LoadDataAsync<G>(string _query, CancellationToken _ct = default, params IParameter[] _pmts);
        Task<IEnumerable<G>> LoadDataAsync<G>(string _query, CancellationToken _ct = default);

#endif

    }
}

#endif
#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Data;
using System.Threading.Tasks;
#endif


using System.Collections.Generic;
using System.Threading;

namespace imL.DB
{
    public partial interface IHelper
    {
        Task<IAppReturn<object>> ExecuteAsync(string? _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default, params IParameter[] _params);
        Task<IAppReturn<object>> ExecuteAsync(string? _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default);
        Task<IEnumerable<IAppReturn<object>>> ExecutionsAsync(string? _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default, params IParameter[][] _params);
        Task<IEnumerable<IAppReturn<object>>> ExecutionsAsync(string? _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default);

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        Task<DataTable> LoadDataTableAsync(string? _query, CancellationToken _ct = default, params IParameter[] _params);
        Task<DataTable> LoadDataTableAsync(string? _query, CancellationToken _ct = default);
        Task<DataSet> LoadDataSetAsync(string? _query, CancellationToken _ct = default, params IParameter[] _params);
        Task<DataSet> LoadDataSetAsync(string? _query, CancellationToken _ct = default);
        Task<IEnumerable<G>> LoadDataAsync<G>(string? _query, CancellationToken _ct = default, params IParameter[] _params);
        Task<IEnumerable<G>> LoadDataAsync<G>(string? _query, CancellationToken _ct = default);

#endif

    }
}

#endif
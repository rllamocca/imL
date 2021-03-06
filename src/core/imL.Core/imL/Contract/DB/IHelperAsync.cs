#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Data;
#endif

using System;
using System.Threading.Tasks;

using imL.Enumeration.DB;

namespace imL.Contract.DB
{
    public interface IHelperAsync
    {
        IConnection Connection { get; }
        bool Throw { get; }
        IProgress<int> Progress { get; }

        Task<Return> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts);
        Task<Return[]> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _pmts);

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        Task<DataTable> LoadDataTableAsync(string _query, params IParameter[] _pmts);
        Task<DataSet> LoadDataSetAsync(string _query, params IParameter[] _pmts);
        Task<G[]> LoadDataAsync<G>(string _query, params IParameter[] _pmts);

#endif

    }
}

#endif
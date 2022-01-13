#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

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

#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false

        Task<Return> LoadDataTableAsync(string _query, params IParameter[] _pmts);
        Task<Return> LoadDataSetAsync(string _query, params IParameter[] _pmts);

#endif

    }
}

#endif
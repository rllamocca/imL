#if (NET35 || NET40) == false

using System;
using System.Threading.Tasks;

using imL.Enumeration.DB;

namespace imL.Contract.DB
{
    public interface IAsyncHelper
    {
        IConnection Connection { get; }
        bool Throw { get; }
        IProgress<int> Progress { get; }

        Task<Return> Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts);
        Task<Return[]> Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _pmts);

#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false

        Task<Return> LoadData(string _query, bool _dataset = true, params IParameter[] _pmts);

#endif

    }
}

#endif
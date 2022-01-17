#if (NET35 || NET40)
using imL.Contract;
#else
using System;
#endif
#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Data;
#endif

using imL.Enumeration.DB;

namespace imL.Contract.DB
{
    public interface IHelper
    {
        IConnection Connection { get; }
        bool Throw { get; }
        IProgress<int> Progress { get; }

        Return Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts);
        Return[] Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _pmts);

#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        DataTable LoadDataTable(string _query, params IParameter[] _pmts);
        DataSet LoadDataSet(string _query, params IParameter[] _pmts);
        G[] LoadData<G>(string _query, params IParameter[] _pmts);

#endif
    }
}

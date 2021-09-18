using System;

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

#if ( NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3) == false

        Return LoadData(string _query, bool _dataset = true, params IParameter[] _pmts);

#endif
    }
}

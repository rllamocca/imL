#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System;
using System.Data;
#endif


using System;

namespace imL.DB
{
    public partial interface IHelper
    {
        IConnection Connection { get; }
        bool? Throw { get; }
        IProgress<int> Progress { get; }
    }
}

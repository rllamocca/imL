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
using System;

namespace imL
{
    public interface IAppTrack : IAppReturn
    {
        DateTime? ExecutionStart { get; set; }
        DateTime? ExecutionStop { get; set; }
        TimeSpan? ExecutionTime { get; set; }
        string GUID { get; set; }

        void Complement(IAppReturn _result);
    }

    public interface IAppTrack<G> : IAppReturn<G>, IAppReturn, IAppTrack
    {
        void Complement(IAppReturn<G> _result);
    }
}

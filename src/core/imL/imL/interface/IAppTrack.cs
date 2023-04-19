using System;

namespace imL
{
    public interface ITrack : IReturn
    {
        DateTime? ExecutionStart { get; set; }
        DateTime? ExecutionStop { get; set; }
        TimeSpan? ExecutionTime { get; set; }
        string? GUID { get; set; }

        void Complement(IReturn _result);
    }

    public interface ITrack<G> : IReturn<G>, IReturn, ITrack
    {
        void Complement(IReturn<G> _result);
    }
}

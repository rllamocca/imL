using imL.Enumeration;

namespace imL.Contract
{
    public interface IAppReturn
    {
        EReturn? Type { get; set; }
        string Message { get; set; }
        string Method { get; set; }
    }

    public interface IAppReturn<G> : IAppReturn
    {
        G Result { set; get; }
    }
    public interface IAppReturn<GKey, GValue> : IAppReturn
    {
        GKey Key { set; get; }
        GValue Value { set; get; }
    }
}

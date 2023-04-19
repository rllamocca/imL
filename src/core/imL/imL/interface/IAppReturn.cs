namespace imL
{
    public interface IReturn
    {
        EReturn? Type { get; set; }
        string? Message { get; set; }
        string? Method { get; set; }
    }

    public interface IReturn<G> : IReturn
    {
        G? Result { set; get; }
    }
    public interface IReturn<GKey, GValue> : IReturn
    {
        GKey? Key { set; get; }
        GValue? Value { set; get; }
    }
}

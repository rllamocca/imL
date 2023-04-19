namespace imL
{
    public sealed class ReturnDefault : IReturn
    {
        public EReturn? Type { set; get; }
        public string? Message { set; get; }
        public string? Method { set; get; }
    }

    public sealed class ReturnDefault<G> : IReturn<G>, IReturn
    {
        public G? Result { set; get; }

        public EReturn? Type { set; get; }
        public string? Message { set; get; }
        public string? Method { set; get; }
    }

    public sealed class ReturnDefault<GKey, GValue> : IReturn<GKey, GValue>, IReturn
    {
        public GKey? Key { set; get; }
        public GValue? Value { set; get; }

        public EReturn? Type { set; get; }
        public string? Message { set; get; }
        public string? Method { set; get; }
    }
}

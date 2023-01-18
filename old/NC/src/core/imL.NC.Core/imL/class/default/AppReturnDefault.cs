namespace imL
{
    public class AppReturnDefault : IAppReturn
    {
        public EReturn? Type { set; get; }
        public string Message { set; get; }
        public string Method { set; get; }
    }

    public class AppReturnDefault<G> : IAppReturn<G>, IAppReturn
    {
        public G Result { set; get; }

        public EReturn? Type { set; get; }
        public string Message { set; get; }
        public string Method { set; get; }
    }

    public class AppReturnDefault<GKey, GValue> : IAppReturn<GKey, GValue>, IAppReturn
    {
        public GKey Key { set; get; }
        public GValue Value { set; get; }

        public EReturn? Type { set; get; }
        public string Message { set; get; }
        public string Method { set; get; }
    }
}

namespace imL
{
    public class LockedBase
    {
        protected static readonly object _LOCK = new object();

        static IAppInfo _APP;
        public static IAppInfo App { get { lock (_LOCK) { return _APP; } } }

        public static void Load(IAppInfo _app)
        {
            _APP = _app;
        }
    }
}
namespace imL.Contract
{
    public class LockedBase
    {
        protected static readonly object _LOCKED = new object();

        static IAppInfo _APP;
        public static IAppInfo App { get { lock (LockedBase._LOCKED) { return LockedBase._APP; } } }

        public static void Load(IAppInfo _app)
        {
            LockedBase._APP = _app;
        }
    }
}
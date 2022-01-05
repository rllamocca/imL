namespace imL.Contract
{
    public class BLocked
    {
        protected static readonly object _LOCKED = new object();

        private static IAppInfo _APP;
        public static IAppInfo App { get { lock (BLocked._LOCKED) { return BLocked._APP; } } }

        public static void Load(IAppInfo _app)
        {
            BLocked._APP = _app;
        }
    }
}
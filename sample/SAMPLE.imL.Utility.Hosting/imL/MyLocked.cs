using System.IO;
using System.Text.Json;

using imL.Contract;

using NLog;

//using NAMESPACE_PROBLEM = imL.Contract;

namespace SAMPLE.imL.Utility.Hosting
{
    internal sealed class MyLocked : LockedBase
    {
        private static readonly Logger _LOGGER = LogManager.GetCurrentClassLogger();
        private static MySettings _SETTING;

        public static Logger Logger { get { lock (MyLocked._LOCKED) { return MyLocked._LOGGER; } } }
        public static MySettings Setting { get { lock (MyLocked._LOCKED) { return MyLocked._SETTING; } } }

        public static new void Load(IAppInfo _app)
        {
            LockedBase.Load(_app);

            MyLocked._SETTING = JsonSerializer.Deserialize<MySettings>(File.ReadAllText(Path.Combine(MyLocked.App.Path, "settings.json")));
            //AppLocked._HTTP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _conf.User, _conf.Password))));
        }
    }
}

using System.IO;
using System.Text.Json;

using imL.Contract;

using NLog;

//using NAMESPACE_PROBLEM = imL.Contract;

namespace SAMPLE.imL.Utility.Hosting
{
    internal sealed class Locked : BLocked
    {
        private static readonly Logger _LOGGER = LogManager.GetCurrentClassLogger();
        private static Settings _SETTING;

        public static Logger Logger { get { lock (Locked._LOCKED) { return Locked._LOGGER; } } }
        public static Settings Setting { get { lock (Locked._LOCKED) { return Locked._SETTING; } } }

        public static new void Load(IAppInfo _app)
        {
            BLocked.Load(_app);

            Locked._SETTING = JsonSerializer.Deserialize<Settings>(File.ReadAllText(Path.Combine(Locked.App.Path, "settings.json")));
            //AppLocked._HTTP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _conf.User, _conf.Password))));
        }
    }
}

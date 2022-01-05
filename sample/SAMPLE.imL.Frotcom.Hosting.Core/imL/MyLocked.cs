using System.Text.Json;

using imL.Contract;
using imL.Frotcom.Hosting.Core;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal sealed class MyLocked : BAppLocked
    {
        private static MySettings? _SETTING;

        public static MySettings? Setting { get { lock (MyLocked._LOCKED) { return MyLocked._SETTING; } } }

        public static new void Load(IAppInfo _app)
        {
            BAppLocked.Load(_app);

            MyLocked._SETTING = JsonSerializer.Deserialize<MySettings>(File.ReadAllText(Path.Combine(MyLocked.App.Path, "settings.json")));
            //AppLocked._HTTP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _conf.User, _conf.Password))));
        }
    }
}

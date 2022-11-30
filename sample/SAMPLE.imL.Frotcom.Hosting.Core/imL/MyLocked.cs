using System.Text.Json;

using imL.Contract;
using imL.Frotcom.Hosting.Core;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal sealed class MyLocked : LockedHost
    {
        private static MySettings? _SETTING;

        public static MySettings Setting { get { lock (_LOCKED) { return _SETTING; } } }

        public static new void Load(IAppInfo _app)
        {
            LockedHost.Load(_app);

            _SETTING = JsonSerializer.Deserialize<MySettings>(File.ReadAllText(Path.Combine(MyLocked.App.Path, "settings.json")));

            if (_SETTING == null)
                throw new ArgumentNullException(nameof(_SETTING));

            //AppLocked._HTTP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _conf.User, _conf.Password))));
        }
    }
}

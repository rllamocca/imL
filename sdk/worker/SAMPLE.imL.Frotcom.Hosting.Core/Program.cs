using System.Text.Json;

using imL;
using imL.Frotcom.Hosting.Core;
using imL.Rest.Frotcom;

using SAMPLE.imL.Frotcom.Hosting.Core;

IAppInfo _info = new AppInfoDefault(args);
MySetting? _SETTING = JsonSerializer.Deserialize<MySetting>(File.ReadAllText(Path.Combine(_info.Base, "settings.json")));

if (_SETTING == null)
    throw new ArgumentNullException(nameof(_SETTING));

await HostHelperAsync.ConsoleAsync<MyExecution, MyWorker>(new AppInfoDefault(args), _SETTING);

FrotcomClient.Dispose();
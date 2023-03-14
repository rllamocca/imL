using imL;
using imL.Frotcom.Hosting.Core;

using SAMPLE.imL.Frotcom.Hosting.Core;

MyLocked.Load(new AppInfoDefault(args));

await HostHelperAsync.ConsoleAsync<MyExecution, MyWorker>(MyLocked.Setting, MyLocked.App);
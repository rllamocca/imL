using imL.Frotcom.Hosting.Core;
using imL.Fulfill;

using SAMPLE.imL.Frotcom.Hosting.Core;

//MyLocked _al = new();
//_al?.Try(new AppInfoDefault(args));
MyLocked.Load(new AppInfoDefault(args));

await AppRunAsync.ConsoleAsync<MyExecution, MyWorker>(MyLocked.Setting?.Hosted, MyLocked.App);
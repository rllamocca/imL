// See https://aka.ms/new-console-template for more information
using imL.Tool.Terminal;

using SAMPLE.imL.Tool.Terminal;

await TerminalHelperAsync.RunAsync<MySettings>(MyProgram.DoWork, args, "https://aka.ms/new-console-template", "waka.ms");
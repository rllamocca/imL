// See https://aka.ms/new-console-template for more information
using imL.Tool.Terminal;

using SAMPLE.imL.Tool.Terminal;

Console.WriteLine(Environment.ProcessId);

await TerminalHelper.RunAsync<MySetting>(MyWork.DoWork, args, "https://aka.ms/new-console-template", "aka.ms");
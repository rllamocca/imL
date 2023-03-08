// See https://aka.ms/new-console-template for more information
using imL.Tool.Terminal;

using SAMPLE.imL.Tool.Terminal;

DateTime _now = DateTime.Now;
string _format = "yyyy'-'MM'-'dd' T'HH':'mm':'ss' 'zzz";

Console.WriteLine(Environment.ProcessId);
Console.WriteLine(_now.ToString("u"));
Console.WriteLine(_now.ToString(_format));
Console.WriteLine(_now.ToUniversalTime().ToString("u"));
Console.WriteLine(_now.ToUniversalTime().ToString(_format));

await TerminalHelper.RunAsync<MySetting>(MyWork.DoWork, args, "https://aka.ms/new-console-template", "aka.ms");
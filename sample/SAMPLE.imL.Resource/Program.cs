// See https://aka.ms/new-console-template for more information

using imL.Contract;
using imL.Resource;

IProcessInfo _process = new ProcessInfoDefault(new AppInfoDefault(args));
_process.AddInserted(1);
_process.Danger(new ArgumentNullException("Richie"));
string _body = HtmlPattern.Resume(_process);

Console.WriteLine(_body);
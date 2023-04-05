// See https://aka.ms/new-console-template for more information

using imL;
using imL.Resource;

IProcessInfo _process = new ProcessInfoDefault(new AppInfoDefault(args));
_process.AddInserted();
_process.Danger(new ArgumentNullException("Richie"));
string _body = HtmlPattern.Resume(_process);

Console.WriteLine(_body);
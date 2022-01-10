// See https://aka.ms/new-console-template for more information

using imL.Contract;
using imL.Resource;
using imL.Utility;

Console.WriteLine("Hello, World!");

IProcessInfo _process = new ProcessInfoDefault();
TerminalHelper.Starts();

_process.AddInserted(1);
_process.Danger(new ArgumentNullException("Richie"));
string _body = HtmlPattern.Resume(_process);

if (true)
{

}
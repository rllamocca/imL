// See https://aka.ms/new-console-template for more information

using imL;

Console.WriteLine("Hello, World!");
ConsoleHelper.Begins();
/*
Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);
*/

string _format = "###'.'###'.'###'.'###'.'###'.'###'.'###'.'###'-'#";
_format = "###.###.###.###.###.###.###.###-#";

Formatter _f = new(_format, false, new char[] { '0' });

//Console.WriteLine((246159637).ToString(_format));
Console.WriteLine(_f.Enforce("246159637"));
Console.WriteLine(_f.Enforce("123456789"));
Console.WriteLine(_f.Enforce("123456780"));
Console.WriteLine(_f.Enforce("123456781"));
Console.WriteLine(_f.Enforce("123456782"));
Console.WriteLine(_f.Enforce("123456783"));
Console.WriteLine(_f.Enforce("0246159637"));
Console.WriteLine(_f.Enforce("00246159637"));
Console.WriteLine(_f.Enforce("000246159637"));

MemoryUnit _mu = new(1, EMemoryUnit.MB);
MemoryUnit _mub = _mu.To(EMemoryUnit.KB);
MemoryUnit _muc = _mu.To(EMemoryUnit.GB);
MemoryUnit _mud = _mu.To(EMemoryUnit.Byte);
MemoryUnit _mue = _mu.To();

MemoryUnit _mu20 = new(20, EMemoryUnit.MB);

Console.WriteLine(_mu);
Console.WriteLine(_mub);
Console.WriteLine(_muc);
Console.WriteLine(_mud);
Console.WriteLine(_mue);
Console.WriteLine(_mu == _mu20);
Console.WriteLine(_mu != _mu20);
Console.WriteLine(_mu < _mu20);
Console.WriteLine(_mu <= _mu20);
Console.WriteLine(_mu > _mu20);
Console.WriteLine(_mu >= _mu20);
Console.WriteLine((_mu - _mu20).To());
Console.WriteLine((_mu + _mu20).To());

using (Progress32 _pb = new(10, EReportProgress.StartsAtZero))
{
    for (int _i = 0; _i < 10; _i++)
    {
        IProgress<int> _pb2 = new Progress32(15, EReportProgress.StartsAtZero, _pb);

        for (int _j = 0; _j < 15; _j++)
        {
            Thread.Sleep(200);
            _pb2.Report(_j);
        }

        Thread.Sleep(800);
        _pb.Report(_i);
    }
}

ConsoleHelper.Ends(true);

/*
//CTRL_C_EVENT
//CTRL_BREAK_EVENT
static void CancelKeyPress(object _sender, ConsoleCancelEventArgs _args)
{
    Console.WriteLine("CancelKeyPress ...");
    Console.WriteLine($" SpecialKey pressed: {_args.SpecialKey}\n");

    //CTRL_LOGOFF_EVENT
    //CTRL_SHUTDOWN_EVENT

    //Console.WriteLine($"  Cancel property: {_args.Cancel}");
    // Set the Cancel property to true to prevent the process from terminating.
    //_args.Cancel = true;
}

//CTRL_CLOSE_EVENT
static void ProcessExit(object _sender, EventArgs _args)
{
    Console.WriteLine("ProcessExit ...");
}
*/
// See https://aka.ms/new-console-template for more information

using imL.Enumeration;
using imL.Sealed;
using imL.Utility;

Console.WriteLine("Hello, World!");
TerminalHelper.Starts();
/*
Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);
*/

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

TerminalHelper.Ends(true);

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
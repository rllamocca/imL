using System;
using System.Threading;

using imL.Utility.Terminal;
using imL.Utility.Terminal.Fulfill;

namespace TEST.imL.Utility.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.Starts();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

            using (FProgress32 _pb = new(10))
            {
                for (int i = 0; i < 10; i++)
                {
                    using (FProgress32 _pb2 = new(15, _pb))
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            _pb2.Report(j);
                            Thread.Sleep(200);
                        }
                    }
                    _pb.Report(i);
                    Thread.Sleep(200);
                }
            }

#if DEBUG
            ConsoleHelper.PressAnyKeyToExit();
#else
            ConsoleHelper.Ends(true);
#endif
        }

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

    }
}
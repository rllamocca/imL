﻿using System;

using imL.Utility.Terminal;

namespace TEST.imL.Utility.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.Starts();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

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
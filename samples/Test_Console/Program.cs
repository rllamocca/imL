﻿using imL.Utility;
using imL.Utility.Ftp;
using imL.Utility.Terminal;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Test_Console
{
    class Program
    {
        /*
CTRL_C_EVENT
CTRL_BREAK_EVENT
*/
        static void CancelKeyPress(object _sender, ConsoleCancelEventArgs _args)
        {
            Console.WriteLine("CancelKeyPress ...");
            Console.WriteLine($" SpecialKey pressed: {_args.SpecialKey}\n");

            /*
CTRL_LOGOFF_EVENT
CTRL_SHUTDOWN_EVENT
             */

            //Console.WriteLine($"  Cancel property: {_args.Cancel}");
            // Set the Cancel property to true to prevent the process from terminating.
            //_args.Cancel = true;
        }

        /*
CTRL_CLOSE_EVENT
        */
        static void ProcessExit(object _sender, EventArgs _args)
        {
            Console.WriteLine("ProcessExit ...");
        }

        async static Task Main(
            //string[] _args
            )
        {
            ConsoleHelper.Starts();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

            //################################################################

            using (ElapsedTime _time = new())
            {
                FormatFtp _tmp3 = new();
                _tmp3.Timeout = Convert.ToInt32(TimeSpan.FromSeconds(40).TotalMilliseconds);
                _tmp3.Host = "ftp://ftp1.microexpertos.cl";
                _tmp3.Path = "/CV_BCI_RDA_20210729.csv";
                _tmp3.UserName = "cruzverde";
                _tmp3.Password = "CruzVerde2016.";

                Stream _ms = new MemoryStream();
                FtpHelper.From_DownloadFile(ref _ms, _tmp3);
                _ms.FileCreate("C:\\tmp\\CV_BCI_RDA_20210729.csv");
            }
            //################################################################

#if DEBUG
            ConsoleHelper.Ends(true, false);
#else
            ConsoleHelper.Ends();
#endif
        }
    }
}
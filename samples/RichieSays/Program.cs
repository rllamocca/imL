using imL.Utility;
using imL.Utility.Terminal;
using imL.Utility.Terminal.Process;

ConsoleHelper.Starts();
//################################################################

if (args != null && args.Length > 0)
{
    bool _reorganize = args.ArgAppear("REORGANIZE");

    if (_reorganize)
        Reorganize.Main(args);
}

//################################################################
ConsoleHelper.Ends();
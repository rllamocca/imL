using imL.Utility;

using RichieSays;

TerminalHelper.Starts();
//################################################################

if (args != null && args.Length > 0)
{
    bool _reorganize = args.ArgAppear("REORGANIZE");

    if (_reorganize)
        Reorganize.Start(args);
}

//################################################################
TerminalHelper.Ends();
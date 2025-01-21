using imL;

using RichieSays;

ConsoleHelper.Begins();
//################################################################

if (args != null && args.Length > 0)
{
    bool _reorganize = args.ArgAppear("REORGANIZE");

    if (_reorganize)
        Reorganize.Start(args);
}

//################################################################
ConsoleHelper.Ends();
using System;

namespace imL
{
    [Flags]
    public enum EEndLine
    {
        All = -1,
        None = 0,
        HT = 1, // 9 Horizontal Tab
        LF = 2, // 10 Line Feed
        VT = 4, // 11 Vertical Tab
        FF = 8, // 12 Form Feed
        CR = 16, // 13 Carriage Return

        //All = HT | LF | VT | FF | CR
    }
}

namespace imL.Enumeration
{
    public enum EMemoryUnit
    {
        bit = 0,
        nibble = 2,
        Byte = 3,
        Word = 4,
        DWord = 5, //double
        QWord = 6, //quadruple
        DQWord = 7,
        KB = 3 + 10, //kilo
        MB = 3 + 10 * 2, //mega
        GB = 3 + 10 * 3, //giga
        TB = 3 + 10 * 4, //terra
        PB = 3 + 10 * 5, //peta
        EB = 3 + 10 * 6, //exa
        ZB = 3 + 10 * 7, //zetta
        YB = 3 + 10 * 8, //yotta
        BB = 3 + 10 * 9, //bronto
        GeB = 3 + 10 * 10, //geop
    }
}
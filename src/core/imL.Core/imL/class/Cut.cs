using System;

namespace imL
{
    public class Cut
    {
        public string Name { get; }
        public int Length { get; }
        public Type Type { get; }
        public bool Trim { get; }
        public object DefaultNull { get; }
    }
}
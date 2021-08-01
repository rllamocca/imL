using System.Text;

namespace imL.Utility
{
    public static class EncodingUtility
    {
        public static void SolutionDefault(ref Encoding _enc)
        {
            if (_enc == null)
                _enc = Encoding.UTF8;
        }
    }
}

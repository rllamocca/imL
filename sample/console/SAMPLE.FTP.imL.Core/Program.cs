#if (NET45_OR_GREATER || NET5_0_OR_GREATER)

using System.Threading.Tasks;

#endif

using System;

using imL.Format;

namespace SAMPLE.FTP.imL.Core
{
    internal partial class Program
    {
        static readonly string _SEP = "================";
        static readonly FtpFormat _FORMAT = new FtpFormat
        {
            Host = null,
            Path = null,
            UserName = null,
            Password = null,
        };

#if (NET45_OR_GREATER || NET5_0_OR_GREATER)
        async static Task Main(string[] args)
        {
            try
            {
#if NET5_0_OR_GREATER
                Console.WriteLine(_SEP);
                await Test_ListDirectoryIAsync(_FORMAT);

                Console.WriteLine(_SEP);
                await Test_ListSubdirectoriesIAsync(_FORMAT);
#else
                Console.WriteLine(_SEP);
                await Test_ListDirectoryAsync(_FORMAT);

                Console.WriteLine(_SEP);
                await Test_ListSubdirectoriesAsync(_FORMAT);
#endif
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }

            Console.WriteLine("end");
        }
#else
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(_SEP);
                Test_ListDirectory(_FORMAT);
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }

            Console.WriteLine("end");
        }
#endif

    }
}

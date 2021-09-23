using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;

namespace TEST.imL.Rest.Frotcom
{
    class Program
    {
        async static Task Main(string[] args)
        {
            try
            {
                FrotcomFormat _format = new();
                _format.URI = "https://v2api.frotcom.com";
                _format.Authorize = new();
                _format.Authorize.provider = "";
                _format.Authorize.username = "";
                _format.Authorize.password = "";

                HttpClient _client = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

                Authorize _token = await FrotcomAsyncHelper.RetriveToken(
                    new FrotcomClient(_format.URI, _client, null),
                    _format,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "token.json")
                    );
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }

            Console.ReadKey();
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;

namespace TEST.imL.Rest.Frotcom
{
    class Program
    {
        async static Task Main()
        {
            try
            {
                FrotcomFormat _format = new();
                _format.URI = "https://v2api.frotcom.com";
                _format.Authorize = new();
                _format.Authorize.provider = "";
                _format.Authorize.username = "";
                _format.Authorize.password = "";

                HttpClient _http = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

                //Authorize _token = await CommonAsync.TokenFiled(
                //    new FrotcomClient(_format.URI, _http, null),
                //    _format.Authorize
                //    );

                FrotcomClient _client = new(_format.URI, _http, null);

                Authorize _token = await FrotcomHelperAsync.AuthorizeUser(_client, _format.Authorize);
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }

            Console.ReadKey();
        }
    }
}

// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Http.Headers;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;

using SAMPLE.imL.Rest.Frotcom;

FrotcomFormat _format = new()
{
    URI = "https://v2api.frotcom.com",
    Authorize = new()
    {
        provider = "thirdparty",
        username = "LrEPRefXyIv7Xzf",
        password = "AuxyD3xShPfLqA1f3GF9YjQP21tG"
    }
};

//HttpClient _http = new();
//HttpClient _http = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
HttpClient _http = new(new MyDelegatingHandler(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }));
_http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

FrotcomClient _client = new(_http, _format);

Authorize _token = await FrotcomHelperAsync.AuthorizeUserAsync(_client);
_client.Token = _token;
Console.WriteLine(_token.token);

//Vehicle[] _vehicles = await FrotcomHelperAsync.GetVehiclesAsync(_client);
//foreach (var _item in _vehicles)
//    Console.WriteLine("{0} {1}", _item.licensePlate, _item.lastCommunication);

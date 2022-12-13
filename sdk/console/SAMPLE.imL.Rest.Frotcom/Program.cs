// See https://aka.ms/new-console-template for more information
using System.Net;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;

FrotcomFormat _format = new();
_format.URI = "https://v2api.frotcom.com";
_format.Authorize = new();
_format.Authorize.provider = "";
_format.Authorize.username = "";
_format.Authorize.password = "";

HttpClient _http = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
FrotcomClient _client = new(_http, _format);
Authorize _token = await FrotcomHelperAsync.AuthorizeUserAsync(_client);

Console.WriteLine(_token.token);
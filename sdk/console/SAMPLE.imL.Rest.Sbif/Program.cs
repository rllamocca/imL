// See https://aka.ms/new-console-template for more information
using System.Net;

using imL.Rest.Sbif;

SbifFormat _format = new();
_format.URI = "https://api.sbif.cl";
_format.ApiKey = "";
HttpClient _http = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
SbifClient _client = new(_http, _format);

//new SbifHelperAsync(EFinancialIndicator.Dolar);
CurrencyIndex[] _indices = await SbifHelperAsync.GetLaterMonthAsync(_client);

foreach (CurrencyIndex _item in _indices)
    Console.WriteLine("{0} : {1}", _item.Date.ToLocalTime().ToShortDateString(), _item.Value);
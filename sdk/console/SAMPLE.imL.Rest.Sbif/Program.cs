// See https://aka.ms/new-console-template for more information

using imL.Rest.SBIF;

var _format = new SBIFFormat();
_format.URI = "";
_format.ApiKey = "";
var _client = SBIFClient.GetSingleton(_format);

var _indices = await _client.GetLaterMonthAsync(EResource.UF);

foreach (var _item in _indices)
    Console.WriteLine("{0} : {1}", _item.Date.ToLocalTime().ToShortDateString(), _item.Value);
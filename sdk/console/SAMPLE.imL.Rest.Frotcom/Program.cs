// See https://aka.ms/new-console-template for more information
using imL.Rest.Frotcom;

FrotcomFormat _format = new()
{
    URI = null,
    User = new()
    {
        provider = "thirdparty",
        username = null,
        password = null
    }
};

var _client = FrotcomClient.GetSingleton(_format);

_client.Authorize = await _client.AuthorizeUserAsync();
Console.WriteLine(_client.Authorize.token);

var _vehicles = await _client.GetVehiclesAsync();
foreach (var _item in _vehicles)
    Console.WriteLine("{0} {1}", _item.licensePlate, _item.lastCommunication);

FrotcomClient.Dispose();
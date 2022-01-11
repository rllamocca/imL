// See https://aka.ms/new-console-template for more information
using System.Net;

using imL.Rest.Sbif;

//using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

try
{
    //using var loggerFactory = LoggerFactory.Create(builder =>
    //{
    //    builder
    //        .AddFilter("Microsoft", LogLevel.Warning)
    //        .AddFilter("System", LogLevel.Warning)
    //        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
    //        .AddConsole();
    //});

    //ILogger logger = loggerFactory.CreateLogger<Program>();
    //logger.LogInformation("Example log message");


    SbifFormat _format = new();
    _format.URI = "https://api.sbif.cl/api-sbifv3/recursos_api";
    _format.ApiKey = "";
    HttpClient _http = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
    SbifClient _client = new(_format.URI, _http, _format.ApiKey);

    //new SbifHelperAsync(EFinancialIndicator.Dolar);
    CurrencyInfo[] _indices = await SbifHelperAsync.GetPosterioresMonth(_client);

    foreach (CurrencyInfo _item in _indices)
        Console.WriteLine("{0} : {1}", _item.Date.ToLocalTime().ToShortDateString(), _item.Value);

}
catch (Exception _ex)
{
    Console.WriteLine(_ex);
}

Console.ReadKey();
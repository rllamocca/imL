﻿// See https://aka.ms/new-console-template for more information
using System.Net;

using imL.Enumeration.Http;
using imL.Rest.InsurDesign;
using imL.Rest.InsurDesign.Schema;
using imL.Utility.Http;

InsurDesignFormat _format = new InsurDesignFormat();
_format.AuthURI = "https://staging.insurdesign.com/api-auth";
_format.URI = "";
_format.Username = "lalala";
_format.Password = "lelele";

HttpClient _http = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
InsurDesignClient _client = new(_http, _format);
Login _token = await InsurDesignHelperAsync.PostLoginAsync(_client);
_http.DefaultRequestHeaders.Authorization = HttpHelperAsync.Authentication(EAuthentication.Token, _token.token);
Beneficiary[] _bens = await InsurDesignHelperAsync.GetBeneficiarioAsync(_client);

foreach (Beneficiary _item in _bens)
    Console.WriteLine(_item.uuid);
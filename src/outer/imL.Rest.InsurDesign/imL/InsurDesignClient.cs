using System.Net.Http;

using imL.Http;
using imL.Rest.InsurDesign.Schema;
using imL.Utility.Http;

namespace imL.Rest.InsurDesign
{
    public class InsurDesignClient
    {
        public HttpClient Http { get; }
        public InsurDesignFormat Format { get; }
        public Login Token { set; get; }

        public InsurDesignClient(HttpClient _http, InsurDesignFormat _format)
        {
            Http = _http;
            Format = _format;

            Http.DefaultRequestHeaders.Authorization = HttpHelperAsync.Authentication(EAuthentication.Basic, _format.Username, _format.Password);
        }
    }
}

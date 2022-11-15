using System.Net.Http;

using imL.Enumeration.Http;
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
            this.Http = _http;
            this.Format = _format;

            this.Http.DefaultRequestHeaders.Authorization = HttpHelperAsync.Authentication(EAuthentication.Basic, _format.Username, _format.Password);
        }
    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using imL.Rest.InsurDesign.Schema;

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

            this.Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", this.Format.Username, this.Format.Password))));
        }
    }
}

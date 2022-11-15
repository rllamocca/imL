using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using imL.Rest.InsurDesign.Schema;
using imL.Utility.Http;

namespace imL.Rest.InsurDesign
{
    public class InsurDesignHelperAsync
    {
        public static async Task<Login> PostLoginAsync(InsurDesignClient _client)
        {
            return await _client.Http.PostAsync<Login>(_client.Format.AuthURI + "/login/");
        }

        public static async Task<Beneficiary> GetBeneficiarioAsync(InsurDesignClient _client, string _uuid_run)
        {
            string _uri = _client.Format.URI + "/beneficiario/{0}/";
            _uri = string.Format(_uri, _uuid_run);

            return await _client.Http.GetAsync<Beneficiary>(_uri);
        }
        public static async Task<Beneficiary[]> GetBeneficiarioAsync(InsurDesignClient _client)
        {
            string _uri = _client.Format.URI + "/beneficiario/";

            return await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client.Http, _uri);
        }
        public static async Task<Beneficiary[]> GetBeneficiarioAltasAsync(InsurDesignClient _client, DateTime _begin, DateTime _end)
        {
            string _uri = _client.Format.URI + "/beneficiario/altas/{0}/{1}/";
            _uri = string.Format(_uri, _begin.ToString("yyyy'-'MM'-'dd"), _end.ToString("yyyy'-'MM'-'dd"));

            return await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client.Http, _uri);
        }
        public static async Task<Beneficiary[]> GetBeneficiarioBajasAsync(InsurDesignClient _client, DateTime _begin, DateTime _end)
        {
            string _uri = _client.Format.URI + "/beneficiario/bajas/{0}/{1}/";
            _uri = string.Format(_uri, _begin.ToString("yyyy'-'MM'-'dd"), _end.ToString("yyyy'-'MM'-'dd"));

            return await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client.Http, _uri);
        }

        static string CheckHttp(string _from, string _to)
        {
            string _https = "https://";
            string _http = "http://";
            StringComparison _c = StringComparison.OrdinalIgnoreCase;

            if (
                (_from.StartsWith(_https, _c) && _to.StartsWith(_https, _c)) ||
                (_from.StartsWith(_http, _c) && _to.StartsWith(_http, _c))
                )
                return _to;

            if (_from.StartsWith(_https, _c))
                return _to.Replace(_http, _https);

            if (_from.StartsWith(_http, _c))
                return _to.Replace(_https, _http);

            return _to;
        }
        static async Task<Beneficiary[]> GetBeneficiarioPreviousAsync(HttpClient _client, string _uri)
        {
            Beneficiario _get = await _client.GetAsync<Beneficiario>(_uri);
            string _re = _get.previous;

            if (_re == null)
                return _get.results;

            _re = CheckHttp(_uri, _re);
            List<Beneficiary> _return = new List<Beneficiary>();
            _return.AddRange(_get.results);
            _return.AddRange(await InsurDesignHelperAsync.GetBeneficiarioPreviousAsync(_client, _re));

            return _return.ToArray();
        }
        static async Task<Beneficiary[]> GetBeneficiarioNextAsync(HttpClient _client, string _uri)
        {
            Beneficiario _get = await _client.GetAsync<Beneficiario>(_uri);
            string _re = _get.next;

            if (_re == null)
                return _get.results;

            _re = CheckHttp(_uri, _re);
            List<Beneficiary> _return = new List<Beneficiary>();
            _return.AddRange(_get.results);
            _return.AddRange(await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client, _re));

            return _return.ToArray();
        }
    }
}

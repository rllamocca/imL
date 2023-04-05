using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using imL.Rest.InsurDesign.Schema;
using imL.Utility.Http;
using imL.Utility;

namespace imL.Rest.InsurDesign
{
    public class InsurDesignHelperAsync
    {
        public static async Task<Login> PostLoginAsync(InsurDesignClient _client)
        {
            return await _client.Http.PostJsonAsync<Login>(_client.Format.AuthURI + "/login/");
        }

        public static async Task<Beneficiary> GetBeneficiarioAsync(InsurDesignClient _client, string _uuid_run)
        {
            string _uri = _client.Format.URI + "/beneficiario/{0}/";
            _uri = string.Format(_uri, _uuid_run);

            return await _client.Http.GetJsonAsync<Beneficiary>(_uri);
        }
        public static async Task<IEnumerable<Beneficiary>> GetBeneficiarioAsync(InsurDesignClient _client)
        {
            string _uri = _client.Format.URI + "/beneficiario/";

            return await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client.Http, _uri);
        }
        public static async Task<IEnumerable<Beneficiary>> GetBeneficiarioAltasAsync(InsurDesignClient _client, DateTime _begin, DateTime _end)
        {
            string _uri = _client.Format.URI + "/beneficiario/altas/{0}/{1}/";
            _uri = string.Format(_uri, _begin.ToString("yyyy'-'MM'-'dd"), _end.ToString("yyyy'-'MM'-'dd"));

            return await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client.Http, _uri);
        }
        public static async Task<IEnumerable<Beneficiary>> GetBeneficiarioBajasAsync(InsurDesignClient _client, DateTime _begin, DateTime _end)
        {
            string _uri = _client.Format.URI + "/beneficiario/bajas/{0}/{1}/";
            _uri = string.Format(_uri, _begin.ToString("yyyy'-'MM'-'dd"), _end.ToString("yyyy'-'MM'-'dd"));

            return await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client.Http, _uri);
        }

        static async Task<IEnumerable<Beneficiary>> GetBeneficiarioPreviousAsync(HttpClient _client, string _uri)
        {
            Beneficiario _get = await _client.GetJsonAsync<Beneficiario>(_uri);
            string _re = _get.previous;

            if (_re == null)
                return _get.results;

            _re = HttpHelper.CheckHttp(_uri, _re);
            List<Beneficiary> _return = new List<Beneficiary>();
            _return.AddRange(_get.results);
            _return.AddRange(await InsurDesignHelperAsync.GetBeneficiarioPreviousAsync(_client, _re));

            return _return.ToArray();
        }
        static async Task<IEnumerable<Beneficiary>> GetBeneficiarioNextAsync(HttpClient _client, string _uri)
        {
            Beneficiario _get = await _client.GetJsonAsync<Beneficiario>(_uri);
            string _re = _get.next;

            if (_re == null)
                return _get.results;

            _re = HttpHelper.CheckHttp(_uri, _re);
            List<Beneficiary> _return = new List<Beneficiary>();
            _return.AddRange(_get.results);
            _return.AddRange(await InsurDesignHelperAsync.GetBeneficiarioNextAsync(_client, _re));

            return _return.ToArray();
        }
    }
}

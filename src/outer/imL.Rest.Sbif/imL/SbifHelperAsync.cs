using System;
using System.Threading.Tasks;

using imL.Rest.Sbif.Schema;

using imL.Utility.Http;

namespace imL.Rest.Sbif
{
    public class SbifHelperAsync
    {
        public SbifHelperAsync(EResource _resource = EResource.UF)
        {
            SbifHelper._RESOURCE = _resource;

            if (SbifHelper._RESOURCE == EResource.None)
                throw new ArgumentOutOfRangeException(nameof(_resource));
        }

        static async Task<CurrencyIndex[]> ReferAsync(SbifClient _client, string _uri)
        {
            switch (SbifHelper._RESOURCE)
            {
                case EResource.None:
                    break;
                case EResource.Dolar:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_Dolar>(_uri))?.Dolares);

                case EResource.Euro:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_Euro>(_uri))?.Euros);

                case EResource.IPC:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_IPC>(_uri))?.IPCs);

                case EResource.TIP:
                    break;
                case EResource.TMC:
                    break;
                case EResource.TAB:
                    break;
                case EResource.UF:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_UF>(_uri))?.UFs);

                case EResource.UTM:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_UTM>(_uri))?.UTMs);

                default:
                    break;
            }

            return null;
        }

        public static async Task<CurrencyIndex[]> GetPreviousYearAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/anteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetPreviousMonthAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/anteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetPreviousAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/anteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }

        public static async Task<CurrencyIndex[]> GetYearAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetMonthAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }

        public static async Task<CurrencyIndex[]> GetLaterYearAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(-1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/posteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetLaterMonthAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(-1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/posteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetLaterAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(-1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/posteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }

        public static async Task<CurrencyIndex[]> GetPeriodYearAsync(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddYears(-1);

            if (_end == null)
                _end = _now.AddYears(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/periodo/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _end?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetPeriodMonthAsync(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddMonths(-1);

            if (_end == null)
                _end = _now.AddMonths(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/periodo/{2}/{3}/{4}/{5}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _begin?.Month.ToString("00"),
                _end?.Year.ToString("0000"),
                _end?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyIndex[]> GetPeriodAsync(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddDays(-1);

            if (_end == null)
                _end = _now.AddDays(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/periodo/{2}/{3}/dias_i/{4}/{5}/{6}/dias_f/{7}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RESOURCE).ToLower(),
                _client.Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _begin?.Month.ToString("00"),
                _begin?.Day.ToString("00"),
                _end?.Year.ToString("0000"),
                _end?.Month.ToString("00"),
                _end?.Day.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
    }
}

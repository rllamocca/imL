using System;
using System.Threading.Tasks;

using imL.Rest.Sbif.imL;
using imL.Rest.Sbif.Schema;

using imL.Utility.Http;

namespace imL.Rest.Sbif
{
    public class SbifHelperAsync
    {
        public SbifHelperAsync(EFinancialIndicator _recurso = EFinancialIndicator.UF)
        {
            SbifHelper._RECURSO = _recurso;

            if (SbifHelper._RECURSO == EFinancialIndicator.None)
                throw new ArgumentOutOfRangeException(nameof(_recurso));
        }

        private static async Task<CurrencyInfo[]> ReferAsync(SbifClient _client, string _uri)
        {
            switch (SbifHelper._RECURSO)
            {
                case EFinancialIndicator.None:
                    break;
                case EFinancialIndicator.Dolar:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_Dolar>(_uri))?.Dolares);

                case EFinancialIndicator.Euro:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_Euro>(_uri))?.Euros);

                case EFinancialIndicator.IPC:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_IPC>(_uri))?.IPCs);

                case EFinancialIndicator.TIP:
                    break;
                case EFinancialIndicator.TMC:
                    break;
                case EFinancialIndicator.TAB:
                    break;
                case EFinancialIndicator.UF:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_UF>(_uri))?.UFs);

                case EFinancialIndicator.UTM:
                    return SbifHelper.Factory((await _client.Http.GetAsync<Recurso_UTM>(_uri))?.UTMs);

                default:
                    break;
            }

            return null;
        }

        public static async Task<CurrencyInfo[]> GetPreviousYearAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/anteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPreviousMonthAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/anteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPreviousAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/anteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }

        public static async Task<CurrencyInfo[]> GetYearAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetMonthAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }

        public static async Task<CurrencyInfo[]> GetLaterYearAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(-1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/posteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetLaterMonthAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(-1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/posteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetLaterAsync(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(-1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/posteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }

        public static async Task<CurrencyInfo[]> GetPeriodYearAsync(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddYears(-1);

            if (_end == null)
                _end = _now.AddYears(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/periodo/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _end?.Year.ToString("0000")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPeriodMonthAsync(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddMonths(-1);

            if (_end == null)
                _end = _now.AddMonths(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/periodo/{2}/{3}/{4}/{5}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
                _client.Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _begin?.Month.ToString("00"),
                _end?.Year.ToString("0000"),
                _end?.Month.ToString("00")
                );

            return await SbifHelperAsync.ReferAsync(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPeriodAsync(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddDays(-1);

            if (_end == null)
                _end = _now.AddDays(1);

            string _uri = _client.Format.URI + "/api-sbifv3/recursos_api/{0}/periodo/{2}/{3}/dias_i/{4}/{5}/{6}/dias_f/{7}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelper._RECURSO).ToLower(),
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

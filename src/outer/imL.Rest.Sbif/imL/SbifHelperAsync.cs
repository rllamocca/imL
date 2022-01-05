using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

using imL.Rest.Sbif.Schema;

using imL.Utility.Http;

namespace imL.Rest.Sbif
{
    public class SbifHelperAsync
    {
        private static readonly CultureInfo _CULTURE = CultureInfo.GetCultureInfo("es-cl");
        private static readonly string _ISO_4217 = (new RegionInfo(SbifHelperAsync._CULTURE.LCID)).ISOCurrencySymbol;
        private static EFinancialIndicator _RECURSO = EFinancialIndicator.UF;
        //public static readonly string _ISO_4217 = "CLP";

        public SbifHelperAsync(EFinancialIndicator _recurso = EFinancialIndicator.UF)
        {
            SbifHelperAsync._RECURSO = _recurso;

            if (SbifHelperAsync._RECURSO == EFinancialIndicator.None)
                throw new ArgumentOutOfRangeException(nameof(_recurso));
        }

        public static CurrencyInfo[] Factory(InternalIndex[] _from)
        {
            List<CurrencyInfo> _return = new List<CurrencyInfo>();

            foreach (InternalIndex _item in _from)
            {
                CurrencyInfo _new = new CurrencyInfo
                {
                    ISO4217 = SbifHelperAsync._ISO_4217,
                    Date = Convert.ToDateTime(_item.Fecha, SbifHelperAsync._CULTURE),
                    Value = Convert.ToDecimal(_item.Valor, SbifHelperAsync._CULTURE)
                };

                _return.Add(_new);
            }

            return _return.ToArray();
        }
        public static async Task<CurrencyInfo[]> Refer(SbifClient _client, string _uri)
        {
            switch (SbifHelperAsync._RECURSO)
            {
                case EFinancialIndicator.None:
                    break;
                case EFinancialIndicator.Dolar:
                    return SbifHelperAsync.Factory((await _client.Http.GetAsync<Recurso_Dolar>(_uri))?.Dolares);

                case EFinancialIndicator.Euro:
                    return SbifHelperAsync.Factory((await _client.Http.GetAsync<Recurso_Euro>(_uri))?.Euros);

                case EFinancialIndicator.IPC:
                    return SbifHelperAsync.Factory((await _client.Http.GetAsync<Recurso_IPC>(_uri))?.IPCs);

                case EFinancialIndicator.TIP:
                    break;
                case EFinancialIndicator.TMC:
                    break;
                case EFinancialIndicator.TAB:
                    break;
                case EFinancialIndicator.UF:
                    return SbifHelperAsync.Factory((await _client.Http.GetAsync<Recurso_UF>(_uri))?.UFs);

                case EFinancialIndicator.UTM:
                    return SbifHelperAsync.Factory((await _client.Http.GetAsync<Recurso_UTM>(_uri))?.UTMs);

                default:
                    break;
            }

            return null;
        }

        public static async Task<CurrencyInfo[]> GetAnterioresYear(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(1);

            string _uri = _client.URI + "/{0}/anteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetAnterioresMonth(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(1);

            string _uri = _client.URI + "/{0}/anteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetAnteriores(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(1);

            string _uri = _client.URI + "/{0}/anteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }

        public static async Task<CurrencyInfo[]> GetYear(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.URI + "/{0}/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetMonth(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.URI + "/{0}/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> Get(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = _client.URI + "/{0}/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }

        public static async Task<CurrencyInfo[]> GetPosterioresYear(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(-1);

            string _uri = _client.URI + "/{0}/posteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPosterioresMonth(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(-1);

            string _uri = _client.URI + "/{0}/posteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPosteriores(SbifClient _client, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(-1);

            string _uri = _client.URI + "/{0}/posteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }

        public static async Task<CurrencyInfo[]> GetPeriodoYear(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddYears(-1);

            if (_end == null)
                _end = _now.AddYears(1);

            string _uri = _client.URI + "/{0}/periodo/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _begin?.Year.ToString("0000"),
                _end?.Year.ToString("0000")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPeriodoMonth(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddMonths(-1);

            if (_end == null)
                _end = _now.AddMonths(1);

            string _uri = _client.URI + "/{0}/periodo/{2}/{3}/{4}/{5}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _begin?.Year.ToString("0000"),
                _begin?.Month.ToString("00"),
                _end?.Year.ToString("0000"),
                _end?.Month.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
        public static async Task<CurrencyInfo[]> GetPeriodo(SbifClient _client, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddDays(-1);

            if (_end == null)
                _end = _now.AddDays(1);

            string _uri = _client.URI + "/{0}/periodo/{2}/{3}/dias_i/{4}/{5}/{6}/dias_f/{7}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(SbifHelperAsync._RECURSO).ToLower(),
                _client.ApiKey,
                _begin?.Year.ToString("0000"),
                _begin?.Month.ToString("00"),
                _begin?.Day.ToString("00"),
                _end?.Year.ToString("0000"),
                _end?.Month.ToString("00"),
                _end?.Day.ToString("00")
                );

            return await SbifHelperAsync.Refer(_client, _uri);
        }
    }
}

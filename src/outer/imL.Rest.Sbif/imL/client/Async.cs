using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using imL.Rest.SBIF.Schema;

using imL.Utility.Http;

namespace imL.Rest.SBIF
{
    public partial class SBIFClient
    {
        async Task<IEnumerable<CurrencyIndex>> ReferAsync(string _uri, EResource _rs = EResource.UF)
        {
            switch (_rs)
            {
                case EResource.None:
                    break;
                case EResource.Dolar:
                    return FactoryISync((await _CLIENT.GetJsonAsync<Recurso_Dolar>(_uri))?.Dolares);

                case EResource.Euro:
                    return FactoryISync((await _CLIENT.GetJsonAsync<Recurso_Euro>(_uri))?.Euros);

                case EResource.IPC:
                    return FactoryISync((await _CLIENT.GetJsonAsync<Recurso_IPC>(_uri))?.IPCs);

                case EResource.TIP:
                    break;
                case EResource.TMC:
                    break;
                case EResource.TAB:
                    break;
                case EResource.UF:
                    return FactoryISync((await _CLIENT.GetJsonAsync<Recurso_UF>(_uri))?.UFs);

                case EResource.UTM:
                    return FactoryISync((await _CLIENT.GetJsonAsync<Recurso_UTM>(_uri))?.UTMs);

                default:
                    break;
            }

            return null;
        }

        public async Task<IEnumerable<CurrencyIndex>> GetPreviousYearAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(1);

            string _uri = "{0}/anteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetPreviousMonthAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(1);

            string _uri = "{0}/anteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetPreviousAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(1);

            string _uri = "{0}/anteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }

        public async Task<IEnumerable<CurrencyIndex>> GetYearAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = "{0}/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetMonthAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = "{0}/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now;

            string _uri = "{0}/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }

        public async Task<IEnumerable<CurrencyIndex>> GetLaterYearAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddYears(-1);

            string _uri = "{0}/posteriores/{2}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetLaterMonthAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddMonths(-1);

            string _uri = "{0}/posteriores/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetLaterAsync(EResource _rs = EResource.UF, DateTime? _date = null)
        {
            if (_date == null)
                _date = DateTime.Now.AddDays(-1);

            string _uri = "{0}/posteriores/{2}/{3}/dias/{4}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _date?.Year.ToString("0000"),
                _date?.Month.ToString("00"),
                _date?.Day.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }

        public async Task<IEnumerable<CurrencyIndex>> GetPeriodYearAsync(EResource _rs = EResource.UF, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddYears(-1);

            if (_end == null)
                _end = _now.AddYears(1);

            string _uri = "{0}/periodo/{2}/{3}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _end?.Year.ToString("0000")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetPeriodMonthAsync(EResource _rs = EResource.UF, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddMonths(-1);

            if (_end == null)
                _end = _now.AddMonths(1);

            string _uri = "{0}/periodo/{2}/{3}/{4}/{5}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _begin?.Month.ToString("00"),
                _end?.Year.ToString("0000"),
                _end?.Month.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }
        public async Task<IEnumerable<CurrencyIndex>> GetPeriodAsync(EResource _rs = EResource.UF, DateTime? _begin = null, DateTime? _end = null)
        {
            DateTime _now = DateTime.Now;

            if (_begin == null)
                _begin = _now.AddDays(-1);

            if (_end == null)
                _end = _now.AddDays(1);

            string _uri = "{0}/periodo/{2}/{3}/dias_i/{4}/{5}/{6}/dias_f/{7}?formato=json&apikey={1}";
            _uri = string.Format(_uri,
                Convert.ToString(_rs).ToLower(),
                Format.ApiKey,
                _begin?.Year.ToString("0000"),
                _begin?.Month.ToString("00"),
                _begin?.Day.ToString("00"),
                _end?.Year.ToString("0000"),
                _end?.Month.ToString("00"),
                _end?.Day.ToString("00")
                );

            return await ReferAsync(_uri, _rs);
        }
    }
}

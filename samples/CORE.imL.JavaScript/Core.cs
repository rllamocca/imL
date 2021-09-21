using System;
using System.Drawing;
using System.Linq;
using System.Threading;

using imL.JavaScript.ChartJS;
using imL.Utility;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CORE.imL.JavaScript
{
    public class Core : Controller
    {

        private static string[] Months(int _length)
        {
            string[] _months = new string[] {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };

            return _months.Take(_length).ToArray();
        }
        private static Color[] MyColors(decimal _per = 0.8m)
        {
            byte _alpha = Convert.ToByte(255 * _per);
            Color[] _colors;

            //_colors = new Color[] {
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#0d6efd")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#6610f2")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#6f42c1")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#d63384")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#dc3545")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#fd7e14")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#ffc107")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#198754")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#20c997")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#0dcaf0"))
            //};

            //_colors = new Color[] {
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#4dc9f6")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#f67019")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#f53794")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#537bc4")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#acc236")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#166a8f")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#00a950")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#58595b")),
            //    Color.FromArgb(_por, ColorTranslator.FromHtml("#8549ba"))
            //};

            _colors = new Color[] {
                Color.FromArgb(_alpha, 255, 99, 132), //red
                Color.FromArgb(_alpha, 255, 159, 64), //orange
                Color.FromArgb(_alpha, 255, 205, 86), //yellow
                Color.FromArgb(_alpha, 75, 192, 192), //green
                Color.FromArgb(_alpha, 54, 162, 235), //blue
                Color.FromArgb(_alpha, 153, 102, 255), //purple
                Color.FromArgb(_alpha, 201, 203, 207) //grey
            };

            //return _colors.Take(_length).ToArray();


            //_colors = new Color[] {
            //    Color.FromArgb(_por, Color.Red),
            //    Color.FromArgb(_por, Color.Green),
            //    Color.FromArgb(_por, Color.Blue),

            //    Color.FromArgb(_por, new Color[] { Color.Red, Color.Green }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Green, Color.Blue }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Blue, Color.Red }.Blend()),

            //    Color.FromArgb(_por, new Color[] { Color.Red, Color.Green, Color.Blue }.Blend())
            //};

            //Color _red = Color.FromArgb(255, 0, 0);
            //Color _green = Color.FromArgb(0, 255, 0);
            //Color _blue = Color.FromArgb(0, 0, 255);

            //_colors = new Color[] {
            //    Color.FromArgb(_por, _red),
            //    Color.FromArgb(_por, _green),
            //    Color.FromArgb(_por, _blue),

            //    Color.FromArgb(_por, new Color[] { _red, _green }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _green, _blue }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _blue, _red }.Blend()),

            //    Color.FromArgb(_por, new Color[] { _red, _green, _blue }.Blend())
            //};


            //_colors = new Color[] {
            //    Color.FromArgb(_por, Color.Cyan),
            //    Color.FromArgb(_por, Color.Magenta),
            //    Color.FromArgb(_por, Color.Yellow),

            //    Color.FromArgb(_por, new Color[] { Color.Cyan, Color.Magenta }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Magenta, Color.Yellow }.Blend()),
            //    Color.FromArgb(_por, new Color[] { Color.Yellow, Color.Cyan }.Blend()),

            //    Color.FromArgb(_por, new Color[] { Color.Cyan, Color.Magenta, Color.Yellow }.Blend())
            //};

            //Color _cyan = Color.FromArgb(0, 176, 246);
            //Color _magenta = Color.FromArgb(245, 0, 135);
            //Color _yellow = Color.FromArgb(255, 233, 0);
            //Color _cyan = Color.FromArgb(0, 235, 245);
            //Color _magenta = Color.FromArgb(245, 0, 235);
            //Color _yellow = Color.FromArgb(245, 235, 0);

            //_colors = new Color[] {
            //    Color.FromArgb(_por, _cyan),
            //    Color.FromArgb(_por, _magenta),
            //    Color.FromArgb(_por, _yellow),

            //    Color.FromArgb(_por, new Color[] { _cyan, _magenta }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _magenta, _yellow }.Blend()),
            //    Color.FromArgb(_por, new Color[] { _yellow, _cyan }.Blend()),

            //    Color.FromArgb(_por, new Color[] { _cyan, _magenta, _yellow }.Blend())
            //};

            return _colors;
        }

        public IActionResult Generic_Post(ILogger _logger, CancellationToken _ct)
        {
            try
            {
                SerieFormat[] _series = new SerieFormat[7];

                for (int _i = 0; _i < _series.Length; _i++)
                {
                    _series[_i] = new SerieFormat(RandomHelper.Decimals(7), string.Format("series {0}", _i));
                }

                Color[] _colors = Core.MyColors(0.2m);

                string[] _axis = Core.Months(7);
                string[] _back = _colors.ToStringRGBA();
                string[] _border = _colors.ToStringRGB();

                //################################################################
                ChartFormat _chart = new()
                {
                    Title = "OtherCharts_PolarArea",
                    Series = _series,
                    XAxis = new()
                    {
                        Name = "axis X",
                        Axis = _axis
                    },
                    BackgroundColor = _back,
                    BorderColor = _border
                };

                //################################################################
                return Ok(ChartJSHelper.OtherCharts_PolarArea(_chart));
            }
            catch (Exception _ex)
            {
                _logger?.LogCritical(_ex, _ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

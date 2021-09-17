using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using imL.JavaScript.ChartJS;
using imL.Utility;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TEST.imL.JavaScript.Models;

namespace TEST.imL.JavaScript.Controllers
{
    public class ChartController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static Color[] colors()
        {
            byte _por = Convert.ToByte(1 * 255);
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
                Color.FromArgb(_por, 255, 99, 132),
                Color.FromArgb(_por, 255, 159, 64),
                Color.FromArgb(_por, 255, 205, 86),
                Color.FromArgb(_por, 75, 192, 192),
                Color.FromArgb(_por, 54, 162, 235),
                Color.FromArgb(_por, 153, 102, 255),
                Color.FromArgb(_por, 201, 203, 207)
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

        public ChartController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //
        [HttpPost]
        [Consumes("application/json", "application/xml", "text/json", "text/xml")]
        public async Task<IActionResult> Post_FB(CancellationToken _ct)
        {
            return await Generic_Post(_ct);
        }

        private async Task<IActionResult> Generic_Post(CancellationToken _ct)
        {
            try
            {
                Color[] _colors = ChartController.colors();

                return Ok(ChartJSHelper.LineCharts_SteppedLineCharts(_colors.ToStringRGB()));
            }
            catch (Exception _ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using imL.JavaScript.ChartJS;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TEST.imL.JavaScript.Models;

namespace TEST.imL.JavaScript.Controllers
{
    public class ChartController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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
                return Ok(ChartJSHelper.LineCharts_SteppedLineCharts());
            }
            catch (Exception _ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

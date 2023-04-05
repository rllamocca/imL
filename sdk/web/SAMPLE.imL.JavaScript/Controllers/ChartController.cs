using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SAMPLE.imL.JavaScript.CORE;
using SAMPLE.imL.JavaScript.Models;

namespace SAMPLE.imL.JavaScript.Controllers
{
    public class ChartController : Controller
    {
        private readonly ILogger<HomeController> _LOGGER;

        public ChartController(ILogger<HomeController> logger)
        {
            this._LOGGER = logger;
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

        //public IActionResult Post_FB(CancellationToken _ct)
        [HttpPost]
        [Consumes("application/json", "application/xml", "text/json", "text/xml")]
        public IActionResult Post_FB()
        {
            Core _core = new();
            return _core.Generic_Post(this._LOGGER);
        }
    }
}

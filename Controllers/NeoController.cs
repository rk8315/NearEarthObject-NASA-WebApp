using Microsoft.AspNetCore.Mvc;
using NearEarthObject_WebApp.Models;
using NearEarthObject_WebApp.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NearEarthObject_WebApp.Controllers
{
    [ApiController]
    public class NeoController : Controller
    {
        private readonly NeoApiService _neoApiService;
        private readonly ILogger<NeoController> _logger;

        public NeoController(NeoApiService neoApiService, ILogger<NeoController> logger)
        {
            _neoApiService = neoApiService;
            _logger = logger;
        }

        [Route("Neo")]
        public async Task<IActionResult> Index(string startDate="2015-09-07", string endDate = "2015-09-08")
        {
            try
            {
                var neos = await _neoApiService.FetchNeoDataAsync(startDate, endDate);
                return View(neos);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, Message = ex.Message });
            }
        }
    }
}

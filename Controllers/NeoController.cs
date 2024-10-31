using Microsoft.AspNetCore.Mvc;
using NearEarthObject_WebApp.Models;
using NearEarthObject_WebApp.Services;

namespace NearEarthObject_WebApp.Controllers
{
    [Route("Neo")]
    public class NeoController : Controller
    {
        private readonly NeoApiService _neoApiService;
        private readonly ILogger<NeoController> _logger;

        public NeoController(NeoApiService neoApiService, ILogger<NeoController> logger)
        {
            _neoApiService = neoApiService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<NearEarthObject>());
        }

        [HttpPost]
        [Route("NeoData")]
        public async Task<IActionResult> FetchData(DateTime startDate, DateTime endDate)
        {
            if((endDate - startDate).TotalDays > 7 || (endDate - startDate).TotalDays < 0)
            {
                TempData["DateError"] = "Dates must be within 7 days of each other and in the correct order.";
                return RedirectToAction("Index"); 
            }

            var neos = await _neoApiService.FetchNeoDataAsync(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            return View("Index", neos);
        }
    }
}

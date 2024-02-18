using ExamErrorPage2.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamErrorPage2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //throw new Exception(Environment.NewLine + Environment.NewLine + "Hata Senaryosu...");
            return View();
        }
        //400 Bad Request
        public IActionResult Scenario1()
        {
            //bad request scenario
            var error = true;
            if (error)
            {
                return new StatusCodeResult(400);
            }
            // other logic
            return View();
        }
        //401 Unauthorized
        public IActionResult Scenario2()
        {
            //authentication scenario
            var IsAuthenticated = false;
            if (!IsAuthenticated)
            {
                return Unauthorized(); // This will return a 401 status code
            }
            // Other logic
            return View();
        }
        //403 Forbidden
        public IActionResult Scenario3()
        {
            //Is there permission scenario
            var IsPermission = false;
            if (!IsPermission)
            {
                return new StatusCodeResult(403);
            }
            // Other logic
            return View();
        }
        //404 Not Found
        public IActionResult Scenario4()
        {
            //IsResource scenario
            var resourceNotFound = true;
            if (resourceNotFound)
            {
                return NotFound();
            }
            // Other logic
            return View();
        }
        //500 Internal Server Error
        public IActionResult Scenario5()
        {
            try
            {
                //Exception throwing scenario
                // ...
                throw new Exception("Hata hata");
                return Ok(); 
            }
            catch (Exception ex)
            {
                // Don't forget to keep a log
                return new StatusCodeResult(500);
            }
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        //This will work in production
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            var exFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exFeature.Error;
            var result = HttpContext.Request.Path;
            ViewBag.Error = exFeature.Endpoint;
            _logger.LogError(exception, "Bir hata meydana geldi.");            

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
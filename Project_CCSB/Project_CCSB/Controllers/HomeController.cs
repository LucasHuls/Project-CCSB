using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_CCSB.Models;
using Project_CCSB.Services;
using System.Diagnostics;

namespace Project_CCSB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult OpeningHours()
        {
            return View();
        }

        public IActionResult Location()
        {
            return View();
        }

        public IActionResult Storage()
        {
            var message = new Message(new string[] { "projectCCSB@gmail.com" }, "Account", "Factuur downloaden", "invoiceEmail");
            _emailSender.SendEmail(message);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

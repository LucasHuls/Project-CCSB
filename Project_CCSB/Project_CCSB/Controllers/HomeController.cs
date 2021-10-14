using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_CCSB.Models;
using Project_CCSB.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            var message = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak gemaakt","Afspraak bevestigd");
            _emailSender.SendEmail(message);
            return View();
        }

        public IActionResult Location()
        {
            return View();
        }

        public IActionResult Storage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

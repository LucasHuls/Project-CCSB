using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using Project_CCSB.Services;
using SelectPdf;
using System.Diagnostics;

namespace Project_CCSB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IContractService _contractService;

        public HomeController(IEmailSender emailSender, IContractService contractService)
        {
            _emailSender = emailSender;
            _contractService = contractService;
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
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        /// <summary>
        /// Sets a new price for vehicle type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetNewPrice(Rate model)
        {
            if (ModelState.IsValid)
            {
                if (model.VehicleType == "Caravan" || model.VehicleType == "Camper")
                {
                    _contractService.SetNewPrice(model);
                    return RedirectToAction("AdminPanel", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kies Caravan of Camper");
                }
            }
            return View("AdminPanel", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

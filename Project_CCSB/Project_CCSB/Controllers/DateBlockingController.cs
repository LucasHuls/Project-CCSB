using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models.ViewModels.Appointment;
using Project_CCSB.Services;

namespace Project_CCSB.Controllers
{
    public class DateBlockingController : Controller
    {
        private readonly IBlockedDatesService _blockedDatesService;

        public DateBlockingController(IBlockedDatesService blockedDatesService)
        {
            _blockedDatesService = blockedDatesService;
        }

        public IActionResult AddBlockedDate()
        {
            return View();
        }
        public IActionResult AllBlockedDates()
        {
            ViewBag.AllBlockedDates = _blockedDatesService.GetBlockedDates();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBlockedDate(BlockedDatesViewModel model)
        {
            _blockedDatesService.StoreDate(model);
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using Project_CCSB.Services;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers
{
    [Authorize]
    public class PdfController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CustomViewRendererService _viewService;
        private readonly string _marksheetFileName;

        public PdfController(ApplicationDbContext db, CustomViewRendererService viewService)
        {
            _db = db;
            _viewService = viewService;
            _marksheetFileName = "PDF.cshtml";
        }

        /// <summary>
        /// Makes a new pdf file with appointments in the next 7 days
        /// </summary>
        /// <returns>Downloadable pdf file</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Pdf()
        {
            DateTime add7Days = DateTime.Now.AddDays(7);

            List<AppointmentVehicleUser> appointmentsIn7Days = (from appointment in _db.Appointments
                                                 join vehicle in _db.Vehicles on appointment.LicensePlate equals vehicle.LicensePlate
                                                 join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                                                 where appointment.Date.Date <= add7Days.Date
                                                 select new AppointmentVehicleUser
                                                 {
                                                     ApplicationUser = user,
                                                     Appointment = appointment,
                                                     Vehicle = vehicle
                                                 })
                                                 .ToList();

            HtmlToPdf pdf = new HtmlToPdf();
            string markSheet = await _viewService
                .RenderViewToStringAsync(ControllerContext,
                                        $"~/Views/Pdf/{_marksheetFileName}",
                                        (appointmentsIn7Days, "Weekoverzicht", "Er zijn geen afspraken de komende 7 dagen"));
            
            PdfDocument doc = pdf.ConvertHtmlString(markSheet);
            var bytes = doc.Save();
            
            return File(bytes, "application/pdf", "Weekoverzicht.pdf");
        }

        /// <summary>
        /// Makes a new pdf file with all appointments
        /// </summary>
        /// <returns>Downloadable pdf file</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PdfAll()
        {
            List<AppointmentVehicleUser> allAppointments = (from appointment in _db.Appointments
                                                                join vehicle in _db.Vehicles on appointment.LicensePlate equals vehicle.LicensePlate
                                                                join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                                                                select new AppointmentVehicleUser
                                                                {
                                                                    ApplicationUser = user,
                                                                    Appointment = appointment,
                                                                    Vehicle = vehicle
                                                                })
                                                                .ToList();

            HtmlToPdf pdf = new HtmlToPdf();
            string markSheet = await _viewService
                .RenderViewToStringAsync(ControllerContext,
                                        $"~/Views/Pdf/{_marksheetFileName}",
                                        (allAppointments, "Alle Afspraken", "Er zijn geen afspraken"));

            PdfDocument doc = pdf.ConvertHtmlString(markSheet);
            var bytes = doc.Save();

            return File(bytes, "application/pdf", "Afspraken.pdf");
        }
    }
}

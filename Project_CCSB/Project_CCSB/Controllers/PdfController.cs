using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_CCSB.Controllers
{
    public class PdfController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public PdfController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Pdf()
        {
            DateTime add7Days = DateTime.Now.AddDays(7);
            List<Appointment> appointmentsIn7Days = _db.Appointments
                                        .Where(x => x.Date.Date <= add7Days)
                                        .ToList();

            HtmlToPdf pdf = new HtmlToPdf();
            string markSheet = string.Empty;
            markSheet = System.IO.File.ReadAllText(_env.WebRootPath + @"\DemoPDF.cshtml");

            markSheet = markSheet
                .Replace("[appointmentType]", appointmentsIn7Days[0].LicensePlate);

            PdfDocument doc = pdf.ConvertHtmlString(markSheet);
            var bytes = doc.Save();

            return File(bytes, "application/pdf", "demo.pdf");
        }
    }
}

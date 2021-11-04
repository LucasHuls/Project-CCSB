using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_CCSB.Models;
using Project_CCSB.Services;
using Rotativa;
using SelectPdf;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project_CCSB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, IWebHostEnvironment env)
        {
            _logger = logger;
            _emailSender = emailSender;
            _env = env;
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
            // Delete appointment 
            var message = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak", "Bekijk Afspraken", "deleteAppointment");
            _emailSender.SendEmail(message);
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
        public IActionResult Pdf()
        {
            HtmlToPdf pdf = new HtmlToPdf();
            string markSheet = string.Empty;
            markSheet = System.IO.File.ReadAllText(_env.WebRootPath + @"\DemoPDF.html");
            StudentMarkSheetModel model = new StudentMarkSheetModel();
            model.SchoolName = "High school";
            model.SchoolAddress = "Jordan";
            model.Email = "h.sarrawy@gmail.com";
            model.SubjectSQL = "SQL server";
            model.SQLMarks = 95;
            model.SubjectC = "C programming";
            model.CMarks = 90;
            model.SubjectEnglish = "Comalsory English";
            model.EnglishMarks = 85;
            markSheet = markSheet.Replace("schoolName", model.SchoolName)
                .Replace("schoolAdress", model.SchoolAddress)
                .Replace("email", model.Email)
                .Replace("subjectSQL", model.SubjectSQL)
                .Replace("sqlMarks", model.SQLMarks.ToString())
                .Replace("subjectC", model.SubjectC)
                .Replace("cMarks", model.CMarks.ToString())
                .Replace("subjectEnglish", model.SubjectEnglish)
                .Replace("englishMarks", model.EnglishMarks.ToString());
            PdfDocument doc = pdf.ConvertHtmlString(markSheet);
            var bytes = doc.Save();

            return File(bytes, "application/pdf", "demo.pdf");
        }
    }
}

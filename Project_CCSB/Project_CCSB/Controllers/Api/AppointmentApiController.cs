using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Services;
using System;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IAppointmentService _appointmentService;

        public AppointmentApiController(IAppointmentService appointmentService, IEmailSender EmailSender )
        {
            _appointmentService = appointmentService;
            _emailSender = EmailSender;
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _appointmentService.AddUpdate(data).Result;
                if (commonResponse.Status == 1)
                {
                    //Successful update
                    commonResponse.Message = Helper.AppointmentUpdated;
                }
                else if (commonResponse.Status == 2)
                {
                    //Successful addition
                    commonResponse.Message = Helper.AppointmentAdded;
                    var message = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak", "Afspraken bekijken", "addAppointment");
                    _emailSender.SendEmail(message);
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpPost]
        [Route("EditAppointment")]
        public IActionResult EditAppointment([FromHeader]string oldDate, AppointmentViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                DateTime formatDate = FormatDate(oldDate);
                int deleted = _appointmentService.DeleteAppointment(formatDate).Result;

                commonResponse.Status = _appointmentService.AddUpdate(data).Result;
                if (commonResponse.Status == 2)
                {
                    //Successful addition
                    commonResponse.Message = Helper.AppointmentUpdated;
                    var message = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak gewijzigd", "Afspraken bekijken", "changeAppointment");
                    _emailSender.SendEmail(message);
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpDelete]
        [Route("DeleteAppointment")]
        public IActionResult DeleteAppointment([FromHeader]string startDate)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                DateTime formatDate = FormatDate(startDate);
                Console.WriteLine(formatDate);

                commonResponse.Status = _appointmentService.DeleteAppointment(formatDate).Result;
                if (commonResponse.Status == 1)
                {
                    // Delete Appointment success
                    commonResponse.Message = "Afspraak is verwijderd";
                    var message = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak verwijderd", "Afspraken bekijken", "deleteAppointment");
                    _emailSender.SendEmail(message);
                }
            } catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 0;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarEvents")]
        public string GetCalendarEvents()
        {
            var appointments = _appointmentService.GetAppointments();
            var json = JsonSerializer.Serialize(appointments);

            return json;
        }

        [HttpGet]
        [Route("GetUserByVehicle/{licensePlate}")]
        public string GetUserByVehicle(string licensePlate)
        {
            return _appointmentService.GetUserByLicensePlate(licensePlate);
        }

        private DateTime FormatDate(string date)
        {
            // Format date string to DateTime (https://stackoverflow.com/questions/31244552/how-to-parse-string-which-contains-gmt-to-datetime)
            DateTime formatDate;
            string dateFormat = "ddd MMM dd yyyy HH:mm:ss 'GMT'K";
            DateTime.TryParseExact(date,
                                    dateFormat,
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out formatDate);
            return formatDate;
        }
    }
}

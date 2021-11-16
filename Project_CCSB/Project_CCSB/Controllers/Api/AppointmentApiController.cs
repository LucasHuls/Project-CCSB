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
        private readonly IContractService _contractService;
        private readonly IUserService _userService;

        public AppointmentApiController(IAppointmentService appointmentService, IEmailSender EmailSender,
            IContractService contractService, IUserService userService )
        {
            _appointmentService = appointmentService;
            _emailSender = EmailSender;
            _contractService = contractService;
            _userService = userService;
        }

        /// <summary>
        /// Function for adding appointments to the calendar. Checks if it already exists and after that it checks if the date is blocked in the BlockedDates table
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveCalendarData")]
        public async Task<IActionResult> SaveCalendarData(AppointmentViewModel data)
        {
            if (_contractService.IsFirstAppointment(data.LicensePlate)) // Check if new contract is needed
            {
                await _contractService.MakeContract(data);
            }

            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _appointmentService.AddUpdate(data).Result;
                if (commonResponse.Status == 1)
                {
                    //Already exists
                    commonResponse.Message = Helper.AppointmentExists;
                }
                else if (commonResponse.Status == 2)
                {
                    //Successful addition
                    commonResponse.Message = Helper.AppointmentAdded;
                    var messageToAdmin = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak", "Afspraken bekijken", "addAppointment");
                    var messageToUser = new Message(new string[] { await _userService.GetUserEmail() }, "Afspraak", "Afspraken bekijken", "addAppointment");
                    _emailSender.SendEmail(messageToAdmin);
                    _emailSender.SendEmail(messageToUser);
                }
                else if (commonResponse.Status == 3)
                {
                    commonResponse.Message = Helper.AppointmentBlocked;
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        /// <summary>
        /// Function for editing appointment. It will send an email to the Admin and User
        /// </summary>
        /// <param name="oldDate"></param>
        /// <param name="data"></param>
        /// <returns>CommonResponse</returns>
        [HttpPost]
        [Route("EditAppointment")]
        public async Task<IActionResult> EditAppointment([FromHeader]string oldDate, AppointmentViewModel data)
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
                    var messageToAdmin = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak gewijzigd", "Afspraken bekijken", "changeAppointment");
                    var messageToUser = new Message(new string[] { await _userService.GetUserEmail() }, "Afspraak gewijzigd", "Afspraken bekijken", "changeAppointment");
                    _emailSender.SendEmail(messageToAdmin);
                    _emailSender.SendEmail(messageToUser);
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = Helper.Failure_code;
            }
            return Ok(commonResponse);
        }

        /// <summary>
        /// Function for removing an appointment. It will send an email to the Admin and User
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns>CommonResponse</returns>
        [HttpDelete]
        [Route("DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment([FromHeader]string startDate)
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
                    var messageToAdmin = new Message(new string[] { "projectCCSB@gmail.com" }, "Afspraak verwijderd", "Afspraken bekijken", "deleteAppointment");
                    var messageToUser = new Message(new string[] { await _userService.GetUserEmail() }, "Afspraak verwijderd", "Afspraken bekijken", "deleteAppointment");
                    _emailSender.SendEmail(messageToAdmin);
                    _emailSender.SendEmail(messageToUser);
                }
            } catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 0;
            }
            return Ok(commonResponse);
        }

        /// <summary>
        /// Function for pulling the calendar events. It check if the user is an admin or user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCalendarEvents")]
        public async Task<string> GetCalendarEvents()
        {
            var userId = _userService.GetUserId();
            var roles = await _userService.GetUserRoles();

            if (roles[0] == "Admin")
            {
                var appointments = _appointmentService.GetAppointments();
                return JsonSerializer.Serialize(appointments);
            } else
            {
                var appointments = _appointmentService.GetUserAppointments(userId);
                return JsonSerializer.Serialize(appointments);
            }
        }

        /// <summary>
        /// Checks the owner of the vehicle by licenseplate
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserByVehicle/{licensePlate}")]
        public string GetUserByVehicle(string licensePlate)
        {
            return _appointmentService.GetUserByLicensePlate(licensePlate);
        }

        /// <summary>
        /// Function for formatting dates.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Formatted Date</returns>
        private DateTime FormatDate(string date)
        {
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

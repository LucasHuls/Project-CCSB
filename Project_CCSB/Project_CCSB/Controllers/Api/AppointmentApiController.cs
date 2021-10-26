using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor, IEmailSender EmailSender )
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
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

        [HttpGet]
        [Route("GetCalendarEvents")]
        public string GetCalendarEvents()
        {
            var appointments = _appointmentService.GetAppointments();
            var json = JsonSerializer.Serialize(appointments);

            return json;
        }
    }
}

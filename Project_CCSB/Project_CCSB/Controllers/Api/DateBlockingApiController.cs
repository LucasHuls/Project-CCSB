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
    public class DateBlockingApiController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlockedDatesService _blockedDatesService;

        public DateBlockingApiController(IUserService userService, IBlockedDatesService blockedDatesService)
        {
            _userService = userService;
            _blockedDatesService = blockedDatesService;
        }

        /// <summary>
        /// Function for removing an blockeddate.
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns>CommonResponse</returns>
        [HttpDelete]
        [Route("DeleteBlockedDate")]
        public async Task<IActionResult> DeleteBlockedDate([FromHeader] string startDate)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                DateTime formatDate = FormatDate(startDate);

                commonResponse.Status = _blockedDatesService.DeleteBlockedDate(formatDate).Result;
                if (commonResponse.Status == 1)
                {
                    // Delete blockeddate success
                    commonResponse.Message = "BLOCKEDDATE is verwijderd";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 0;
            }
            return Ok(commonResponse);
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

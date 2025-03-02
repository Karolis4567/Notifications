using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Exceptions;
using Notifications.Application.Interfaces;

namespace Notifications.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class CompanyNotificationController : Controller
    {
        private ICompanyNotificationService companyNotificationService;
        private ILogger<CompanyNotificationController> logger;

        public CompanyNotificationController(ICompanyNotificationService companyNotificationService, ILogger<CompanyNotificationController> logger)
        {
            this.companyNotificationService = companyNotificationService;
            this.logger = logger;
        }

        /// <summary>
        /// Returns only rules information for Company - NotificationSchedules assigments
        /// </summary>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetCompanyNotificationsScheduleAssignRulesAsync()
        {
            try
            {
                var result = await this.companyNotificationService.GetCompanyNotificationsScheduleAssignRulesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(400);
            }

        }

        /// <summary>
        /// Returns companyId with notification schedule
        /// </summary>
        /// <param name="companyId">Guid companyId</param>
        /// <param name="date">Optional parameter, if null - notification shown for current date, not null notifications shown for provided date </param>
        /// <returns>companyId with notification schedule</returns>

        [HttpGet]
        public async Task<IActionResult> GetCompanyNotificationSchedulesAsync(Guid companyId, DateOnly? date)
        {
            try
            {
                var result = await this.companyNotificationService.GetCompanyNotificationSchedulesAsync(companyId, date);
                return Ok(result);
            }
            catch (CustomValidationException ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(400);
            }
        }
    }
}

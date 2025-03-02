using Microsoft.AspNetCore.Mvc;
using Notifications.Application.DTOs;
using Notifications.Application.Exceptions;
using Notifications.Application.Interfaces;
using Notifications.Application.Services;

namespace Notifications.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompanyController : Controller
    {
        private ICompanyServices companyServices;
        private ILogger<CompanyController> logger;

        public CompanyController(ICompanyServices companyServices, ILogger<CompanyController> logger)
        { 
            this.companyServices = companyServices;
            this.logger = logger;   
        }

        /// <summary>
        /// Creates company
        /// CompanyTypeCode -> (s)small, (m) medium, (l)large
        /// CompanyMarketCode -> (DK) Denmark, (NO) Norway, (SE) Sweden, (FI) Finland
        /// </summary>
        /// <param name="companyCreateDto">Parameter object</param>
        /// <returns></returns>

        
        [HttpPost]
        public async Task<IActionResult> CreateCompanyAsync(CompanyCreateDto companyCreateDto)
        {
            try
            {
                await this.companyServices.CreateCompanyAsync(companyCreateDto);
                return StatusCode(201);
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

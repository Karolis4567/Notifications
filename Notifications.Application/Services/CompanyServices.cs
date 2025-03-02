using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.DTOs;
using Notifications.Application.Exceptions;
using Notifications.Application.Interfaces;
using Notifications.Domain.Entities;
using Notifications.Infrastructure.Data;

namespace Notifications.Application.Services
{
    public class CompanyServices : ICompanyServices
    {
        private NotificationsAppContext context;
        private IMapper mapper;
        private ISharedServices sharedServices;
        public CompanyServices(NotificationsAppContext context, IMapper mapper, ISharedServices sharedServices)
        { 
            this.context = context; 
            this.mapper = mapper;
            this.sharedServices = sharedServices;
        }

        public async Task<int> GetCompanyTypeIdByCodeAsync(string code) =>
            await this.context.CompanyTypes.Where(x => x.companyTypeCode == code)
                .Select(x => x.companyTypeId)
                .FirstOrDefaultAsync();

        public async Task<int> GetCompanyMarketIdByCodeAsync(string code) =>
             await this.context.CompanyMarkets.Where(x => x.companyMarketCode == code)
                .Select(x => x.companyMarketId)
                .FirstOrDefaultAsync();

       public async Task<int> GetNotificationScheduleIdAsync(int companyMarketId, int companyTypeId) =>
            await this.context.CompanyNotificationScheduleAssignsRules
                .Where(x => x.companyMarketId == companyMarketId && x.companyTypeId == companyTypeId)
                .Select(x => x.notificationScheduleId)    
                .FirstOrDefaultAsync();


        public async Task CreateCompanyAsync(CompanyCreateDto companyCreateDto)
        {
            var companyTypeId = await this.GetCompanyTypeIdByCodeAsync(companyCreateDto.CompanyTypeCode);

            if (companyTypeId == 0)
            {
                throw new CustomValidationException($"Invalid CompanyTypeCode {companyCreateDto.CompanyTypeCode}");
            }
            
            var companyMarketId = await this.GetCompanyMarketIdByCodeAsync(companyCreateDto.CompanyMarketCode);

            if (companyMarketId == 0)
            {
                throw new CustomValidationException($"Invalid CompanyMarketCode {companyCreateDto.CompanyMarketCode}");
            }

            var companyAlreadyCreated = await this.sharedServices.IsCompanyAlreadyCreatedAsync(companyCreateDto.CompanyId);
            if (companyAlreadyCreated)
            {
                throw new CustomValidationException("Company already created");
            }

            var entityForInsert = this.mapper.Map<Company>(companyCreateDto);
            entityForInsert.companyMarketId = companyMarketId;
            entityForInsert.companyTypeId = companyTypeId;
            entityForInsert.creationDate = DateTime.Now;
            entityForInsert.updateDate = DateTime.Now;
            await this.context.Companies.AddAsync(entityForInsert);

            var notificationScheduleId = await this.GetNotificationScheduleIdAsync(companyMarketId, companyTypeId);
            if (notificationScheduleId != 0)
            {
                await this.context.CompanyNotificationSchedules.AddAsync(new CompanyNotificationSchedule()
                {
                    companyId = entityForInsert.companyId,
                    notificationScheduleId = notificationScheduleId,
                    creationDate = DateTime.Now,
                    updateDate = DateTime.Now

                });

            }


            await this.context.SaveChangesAsync();

        }

            
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.DTOs;
using Notifications.Application.Exceptions;
using Notifications.Application.Interfaces;
using Notifications.Infrastructure.Data;

namespace Notifications.Application.Services
{
    public class CompanyNotificationService : ICompanyNotificationService
    {
        private NotificationsAppContext context;
        private IMapper mapper;
        private ISharedServices sharedServices;
        public CompanyNotificationService(NotificationsAppContext context, IMapper mapper, ISharedServices sharedServices)
        {
            this.context = context;
            this.mapper = mapper;
            this.sharedServices = sharedServices;   
        }

        public async Task<List<CompanyNotificationsScheduleAssignRulesDto>> GetCompanyNotificationsScheduleAssignRulesAsync()
        {
           var result = await this.context.CompanyNotificationScheduleAssignsRules
                .Include(x => x.companyMarket)
                .Include(x => x.companyType)
                .Include(x => x.notificationSchedule)
                .ProjectTo<CompanyNotificationsScheduleAssignRulesDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task<List<int>> GetCompanyNotificationScheduleDays(Guid companyId)
        {
            var result = await this.context.CompanyNotificationSchedules
              .Include(x => x.notificationSchedule)
              .Where(x => x.companyId == companyId)
              .Select(x => x.notificationSchedule.days)
              .ToListAsync();

            if (result == null || result.Count == 0)
                return new List<int>();

            return result.SelectMany(x => x.Split(","))
                .Select(x => int.TryParse(x, out int parsedValue) ? parsedValue : 0)
                .Where(x => x != 0)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }



        public async Task<CompanyNotificationScheduleDto> GetCompanyNotificationSchedulesAsync(Guid companyId, DateOnly? date)
        {

            DateOnly currentDate = date ?? DateOnly.FromDateTime(DateTime.Now);
            var daysInMonth  = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            
            var isCompanyCreated = await this.sharedServices.IsCompanyAlreadyCreatedAsync(companyId);

            if (!isCompanyCreated)
            {
                throw new CustomValidationException("Company is not created");
            }

            var result = await GetCompanyNotificationScheduleDays(companyId);

            var resultDates = result.Where(x => daysInMonth >= x)
                .Select(x => new DateOnly(currentDate.Year, currentDate.Month, x))
                .ToArray();

            return new CompanyNotificationScheduleDto()
            {
                companyId = companyId, notifications = resultDates
            };

        }
    }
}

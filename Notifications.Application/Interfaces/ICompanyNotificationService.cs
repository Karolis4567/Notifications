using Notifications.Application.DTOs;

namespace Notifications.Application.Interfaces
{
    public interface ICompanyNotificationService
    {
        public Task<List<CompanyNotificationsScheduleAssignRulesDto>> GetCompanyNotificationsScheduleAssignRulesAsync();
        public Task<CompanyNotificationScheduleDto> GetCompanyNotificationSchedulesAsync(Guid companyId, DateOnly? date);
    }
}

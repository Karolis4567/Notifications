using Notifications.Domain.Entities.Interfaces;

namespace Notifications.Domain.Entities
{
    public class CompanyNotificationScheduleAssignRules : ICreationDate, IUpdateDate
    {
        public int companyNotificationScheduleAssignRulesId { get; set; }
        public int companyMarketId { get; set; }
        public CompanyMarket companyMarket { get; set; }
        public int companyTypeId { get; set; }
        public CompanyType companyType { get; set; }
        public int notificationScheduleId { get; set; }
        public NotificationSchedule notificationSchedule { get; set; }
        public DateTime creationDate { get; set; } 
        public DateTime updateDate { get; set; }
    }
}

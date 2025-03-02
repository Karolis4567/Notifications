using Notifications.Domain.Entities.Interfaces;

namespace Notifications.Domain.Entities
{
    public class NotificationSchedule : ICreationDate, IUpdateDate
    {
        public int notificationScheduleId { get; set; }
        public string days { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updateDate { get; set; }
        public ICollection<CompanyNotificationScheduleAssignRules>? scheduleAssignRules { get; set; }
    }
}

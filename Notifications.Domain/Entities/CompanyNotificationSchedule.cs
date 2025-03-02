namespace Notifications.Domain.Entities
{
    public class CompanyNotificationSchedule
    {
        public int companyNotificationsScheduleId { get; set; }
        public Guid companyId { get; set; }
        public Company company { get; set; }

        public int notificationScheduleId { get; set; }
        public NotificationSchedule notificationSchedule { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updateDate { get; set; }
    }
}

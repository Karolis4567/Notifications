namespace Notifications.Application.DTOs
{
    public class CompanyNotificationScheduleDto
    {
        public Guid companyId { get; set; }
        public DateOnly[] notifications { get; set; }
    }
}

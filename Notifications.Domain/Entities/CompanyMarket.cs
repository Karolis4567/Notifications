using Notifications.Domain.Entities.Interfaces;

namespace Notifications.Domain.Entities
{
    public class CompanyMarket : ICreationDate
    {
        public int companyMarketId { get; set; }
        public string companyMarketCode { get; set; }
        public string companyMarketName { get; set; }
        public DateTime creationDate { get; set; }

        public ICollection<Company> companies { get; set; }
        public ICollection<CompanyNotificationScheduleAssignRules>? scheduleAssignRules { get; set; }
    }
}

using Notifications.Domain.Entities.Interfaces;

namespace Notifications.Domain.Entities
{
    public class CompanyType : ICreationDate
    {
        public int companyTypeId { get; set; }  
        public string companyTypeCode { get; set; }
        public string companyTypeName { get; set; }
        public DateTime creationDate { get; set; } 
        public ICollection<Company>? companies { get; set; }
        public ICollection<CompanyNotificationScheduleAssignRules>? scheduleAssignRules { get; set; }

    }
}

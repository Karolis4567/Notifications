using Notifications.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Domain.Entities
{
    public class Company : ICreationDate, IUpdateDate
    {

        public Guid companyId { get; set; }
        public string companyNumber { get; set; }
        public string companyName   { get; set; }

        public int companyTypeId { get; set; }
        public CompanyType companyType { get; set; }
        public int companyMarketId { get; set; }
        public CompanyMarket companyMarket { get; set; }    
        public DateTime creationDate { get; set; }   
        public DateTime updateDate { get; set; }
    }
}

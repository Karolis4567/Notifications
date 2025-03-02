using Notifications.Application.DTOs;

namespace Notifications.Application.Interfaces
{
    public interface ICompanyServices
    {
        public Task CreateCompanyAsync(CompanyCreateDto companyCreateDto);
    }
}

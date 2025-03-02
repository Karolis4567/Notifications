using Microsoft.EntityFrameworkCore;
using Notifications.Application.Interfaces;
using Notifications.Infrastructure.Data;

namespace Notifications.Application.Services
{
    public class SharedServices : ISharedServices
    {
        private NotificationsAppContext context;
        public SharedServices(NotificationsAppContext context) 
        {
            this.context = context;
        }

        public async Task<bool> IsCompanyAlreadyCreatedAsync(Guid id) =>
           await this.context.Companies.AnyAsync(x => x.companyId == id);
    }
}

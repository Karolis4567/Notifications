namespace Notifications.Application.Interfaces
{
    public interface ISharedServices
    {
        public Task<bool> IsCompanyAlreadyCreatedAsync(Guid id);
    }
}

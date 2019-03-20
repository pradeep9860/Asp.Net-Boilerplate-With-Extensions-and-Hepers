using Abp.Application.Services.Dto;
using Application.Helpers.PushNotification.Dto;
using System;
using System.Threading.Tasks;

namespace Application.Helpers.PushNotfication.NotificationLogService
{
    public interface INotificationLogAppService
    {
        Task<int> GetUnReadNotificationCount();
        Task<bool> MarkAsRead(Guid notificationId);
        Task<bool> MarkAllAsRead();
        Task<PagedResultDto<PushNotificationLogDto>> GetPagedAsync(int skipCount, int maxResultCount, string searchText);
        Task<ListResultDto<PushNotificationLogDto>> GetAll(string searchText);
        Task<PagedResultDto<PushNotificationLogDto>> GetUnReadPagedAsync(int skipCount, int maxResultCount, string searchText);
        Task<ListResultDto<PushNotificationLogDto>> GetAllUnRead(string searchText);
    }
}

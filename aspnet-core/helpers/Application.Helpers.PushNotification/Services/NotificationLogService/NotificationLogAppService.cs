using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Application.Helpers.PushNotfication.Models;
using Application.Helpers.PushNotification.Dto;
using Microsoft.EntityFrameworkCore;
using Mongos;
using Newtonsoft.Json;

namespace Application.Helpers.PushNotfication.NotificationLogService
{ 
    public class NotificationLogAppService : PushNotificationAppServiceBase, INotificationLogAppService
    {
        private readonly IRepository<PushNotificationLog, Guid> _repository;
        public NotificationLogAppService(IRepository<PushNotificationLog, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<int> GetUnReadNotificationCount()
        {
           return await _repository.GetAll().CountAsync(x => !x.IsDeleted && !x.IsRead && x.UserId == AbpSession.UserId);
        }

        public async Task<bool> MarkAsRead(Guid notificationId)
        {
            var notification = await _repository.GetAsync(notificationId);
            if(notification == null)
            {
                throw new UserFriendlyException("Invalid Request");
            }
            notification.IsRead = true;
            notification.ReadOn = DateTime.Now;
            await _repository.UpdateAsync(notification);
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAllAsRead()
        {
            var notifications = _repository.GetAll().Where(x => !x.IsDeleted && !x.IsRead && x.UserId == AbpSession.UserId);
            if (!await notifications.AnyAsync())
            {
                return true;
            }
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadOn = DateTime.Now;
                await _repository.UpdateAsync(notification);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultDto<PushNotificationLogDto>> GetPagedAsync(int skipCount, int maxResultCount, string searchText)
        {
            var items = _repository.GetAll().Where(x => !x.IsDeleted && !x.IsRead && x.UserId == AbpSession.UserId);
            if (!string.IsNullOrEmpty(searchText))
            {
                    items = items.Where(x =>
                                 x.NotificationContent.ToLower().Contains(searchText.Trim().ToLower())
                                  
                );
            }

            var totalCount = await items.CountAsync();
            var notifications = await items
                            .Skip(skipCount)
                            .Take(maxResultCount)
                            .ToListAsync();

            var result = MapEntityToDto(notifications);
            return new PagedResultDto<PushNotificationLogDto>(totalCount, result);
        }

        public async Task<ListResultDto<PushNotificationLogDto>> GetAll(string searchText)
        {
            var items = _repository.GetAll().Where(x => !x.IsDeleted && x.UserId == AbpSession.UserId);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x =>
                             x.NotificationContent.ToLower().Contains(searchText.Trim().ToLower())

            );
            }

            var totalCount = await items.CountAsync();
            var notifications = await items.ToListAsync();

            var result = MapEntityToDto(notifications);
            return new ListResultDto<PushNotificationLogDto>(result);
        }

        public async Task<PagedResultDto<PushNotificationLogDto>> GetUnReadPagedAsync(int skipCount, int maxResultCount, string searchText)
        {
            var items = _repository.GetAll().Where(x => !x.IsDeleted && !x.IsRead && x.UserId == AbpSession.UserId);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x =>
                             x.NotificationContent.ToLower().Contains(searchText.Trim().ToLower())

            );
            }

            var totalCount = await items.CountAsync();
            var notifications = await items
                            .Skip(skipCount)
                            .Take(maxResultCount)
                            .ToListAsync();

            var result = MapEntityToDto(notifications);
            return new PagedResultDto<PushNotificationLogDto>(totalCount, result);
        }

        public async Task<ListResultDto<PushNotificationLogDto>> GetAllUnRead(string searchText)
        {
            var items = _repository.GetAll().Where(x => !x.IsDeleted && !x.IsRead && x.UserId == AbpSession.UserId);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x =>
                             x.NotificationContent.ToLower().Contains(searchText.Trim().ToLower())

            );
            }

            var totalCount = await items.CountAsync();
            var notifications = await items.ToListAsync();

            var result = MapEntityToDto(notifications);
            return new ListResultDto<PushNotificationLogDto>(result);
        }

        private List<PushNotificationLogDto> MapEntityToDto(List<PushNotificationLog> entities)
        {
            return entities.Select(entity => new PushNotificationLogDto
            {
                Id = entity.Id,
                IsRead = entity.IsRead,
                NotificationBody = JsonConvert.DeserializeObject<NotificationJsonObj>(entity.NotificationBody),
                NotificationContent = entity.NotificationContent,
                NotificationSentOn = entity.NotificationSentOn,
                ReadOn = entity.ReadOn,
                ReferenceId = entity.ReferenceId,
                UserId = entity.UserId
            }).ToList();
        }
    }
}

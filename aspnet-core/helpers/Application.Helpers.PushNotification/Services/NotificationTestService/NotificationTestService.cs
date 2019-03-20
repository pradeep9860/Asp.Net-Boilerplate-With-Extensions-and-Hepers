using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Application.Helpers.PushNotfication.Models;
using Application.Helpers.PushNotification.Dto;
using Mongos; 

namespace Application.Helpers.PushNotfication.NotificationTestService
{ 
    public class NotificationTestAppService : PushNotificationAppServiceBase, INotificationTestService
    {
        private readonly IPushNotificationHelper _pushNotificationHelper;
        public NotificationTestAppService(IPushNotificationHelper pushNotificationHelper)
        {
            _pushNotificationHelper = pushNotificationHelper;
        }

        public async Task<string> Send(NotificationJsonObj input)
        {
            try
            {
                await _pushNotificationHelper.SendNotification(input);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
         
        } 
    }
}

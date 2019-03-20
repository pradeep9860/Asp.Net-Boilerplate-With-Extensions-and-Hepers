using System;
using System.Linq;
using System.Net; 
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Application.Helpers.PushNotfication.Models;
using Application.Helpers.PushNotification.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Helpers.PushNotfication.NotificationService
{
    public class NotificationAppService : INotificationAppService 
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        private readonly IRepository<DeviceInfo, Guid> _deviceRepository;
        private readonly IRepository<PushNotificationLog, Guid> _notificationLogRepository;

        public NotificationAppService(
            IConfiguration configuration,
            IHostingEnvironment environment,
            IRepository<DeviceInfo, Guid> deviceRepository,
            IRepository<PushNotificationLog, Guid> notificationLogRepository
            )
        {
            _configuration = configuration;
            _environment = environment;

            _deviceRepository = deviceRepository;
            _notificationLogRepository = notificationLogRepository;
        }

        [UnitOfWork]
        public virtual async Task SendNotification(NotificationJsonObj input)
        {
            try
            {
                var appKey = "AIzaSyA0CLo0JhmgpxTvphpUKDDIhaSTkGThtdY";// _configuration["PushNotification:AppKey"];
                var fcmHost = "https://fcm.googleapis.com/fcm/send";

                var allUsers = input.notification.UserIdList;

                foreach (var item in allUsers)
                {
                    //save notification log now
                    var logModel = new PushNotificationLog
                    {
                        UserId = item,
                        IsRead = false,
                        NotificationSentOn = DateTime.Now,
                        NotificationContent = input.notification.body,
                        NotificationBody = JsonConvert.SerializeObject(input)
                    }; 
                    await _notificationLogRepository.InsertAsync(logModel);
                }

                var devices = await _deviceRepository.GetAll().Where(x => !x.IsDeleted && allUsers.Contains(x.UserID)).ToListAsync();

                foreach (var device in devices)
                {
                    var notificationId = string.Empty;
                    var notification = await _notificationLogRepository.GetAll().OrderByDescending(x => x.CreationTime).FirstOrDefaultAsync(x => x.UserId == device.UserID);
                    if(notification != null)
                    {
                        notificationId = notification.Id.ToString();
                    }
                    using (var client = new WebClient())
                    {
                        // Set the header so it knows we are sending JSON.
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Headers[HttpRequestHeader.Authorization] = "key=" + appKey;
                        var jsonobj = new NotificationJsonObj();
                        jsonobj.to = device.RegID.ToString();
                        jsonobj.priority = "high";
                        if (device.PlatformType.ToString().Trim().ToLower() == "ios" || device.PlatformType.ToString().Trim().ToLower() == "web")
                        {
                            jsonobj.priority = "high";
                            var noti = new NotificaionBodyDto();
                            //noti.priority = "high";
                            noti.title = input.Title;
                            noti.body = input.Message;
                            noti.code = "PUSH_NOTIFICATION";
                            //notification.content_available = true;
                            jsonobj.notification = noti;
                        } 

                        var data = new NotificaionBodyDto();
                        data.title = input.Title;
                        data.body = input.Message;
                        data.ReferenceId = string.IsNullOrEmpty(input.notification.ReferenceId) ?  "" : input.notification.ReferenceId.ToString();
                        data.NotificationLogId = string.IsNullOrEmpty(notificationId)? Guid.Empty : new Guid(notificationId);
                        data.code = "PUSH_NOTIFICATION";

                        jsonobj.data = data;

                        //for react like javascript framework -- send in the body
                        jsonobj.data.custom_notification = new
                        {
                            title = input.Title,
                            body = input.Message,
                            sound = "default",
                            priority = "high",
                            show_in_foreground = true,
                            targetScreen = "notification_detail",
                            notificationId = string.IsNullOrEmpty(notificationId) ? Guid.Empty : new Guid(notificationId)
                        };

                        //sent notification
                        try
                        {
                            var body = JsonConvert.SerializeObject(jsonobj);
                            var response = client.UploadString(fcmHost, body);
                        }
                        catch (Exception)
                        {
                            //try to sent notification for all users
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
}

using Abp.Domain.Repositories;
using Application.Helpers.PushNotfication.Models;
using Application.Helpers.PushNotfication.NotificationService;
using Application.Helpers.PushNotification.Dto;
using Microsoft.AspNetCore.Hosting;  
using System;
using System.Threading.Tasks;

namespace Application.Helpers.PushNotfication
{
     
    public class PushNotificationHelper : IPushNotificationHelper 
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly INotificationAppService _notificaionRepository;

        public PushNotificationHelper(IHostingEnvironment hostingEnvironment, INotificationAppService notificaionRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _notificaionRepository = notificaionRepository;
        }

        public async Task SendNotification(NotificationJsonObj model)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            try
            {
                await _notificaionRepository.SendNotification(model);
            }
            catch (Exception ex)
            {
                //just throw
                throw ex;
            }
        }
    }

    public interface IPushNotificationHelper 
    {
        Task SendNotification(NotificationJsonObj model);
    }  
}

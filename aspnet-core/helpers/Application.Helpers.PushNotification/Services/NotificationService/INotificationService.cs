

using Application.Helpers.PushNotfication.Models;
using Application.Helpers.PushNotification.Dto;
using System.Threading.Tasks;

namespace Application.Helpers.PushNotfication.NotificationService
{
    public interface INotificationAppService
    {
        Task SendNotification(NotificationJsonObj input);
    }
}

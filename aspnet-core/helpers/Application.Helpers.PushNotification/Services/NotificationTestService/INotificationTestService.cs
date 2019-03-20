using Application.Helpers.PushNotification.Dto;
using System.Threading.Tasks;

namespace Application.Helpers.PushNotfication.NotificationTestService
{
    public interface INotificationTestService
    {
        Task<string> Send(NotificationJsonObj input); 
    }
}

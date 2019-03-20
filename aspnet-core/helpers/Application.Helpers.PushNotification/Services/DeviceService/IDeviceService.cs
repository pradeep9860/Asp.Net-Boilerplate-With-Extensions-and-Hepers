using Application.Helpers.PushNotification.Dto;
using System.Threading.Tasks;

namespace Application.Helpers.PushNotfication.NotificationService
{
    public interface IDeviceAppService
    {
        Task<DeviceInfoDto> Subscribe(DeviceInfoDto input);
        Task<DeviceInfoDto> Unsubscribe(DeviceInfoDto input);
    }
}

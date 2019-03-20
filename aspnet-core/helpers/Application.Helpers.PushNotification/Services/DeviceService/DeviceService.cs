using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Application.Helpers.PushNotfication.Models;
using Application.Helpers.PushNotification.Dto;
using Mongos; 

namespace Application.Helpers.PushNotfication.NotificationService
{
    [AbpAuthorize]
    public class DeviceAppService : PushNotificationAppServiceBase, IDeviceAppService
    {
        private readonly IRepository<DeviceInfo, Guid> _repository;
        public DeviceAppService(IRepository<DeviceInfo, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<DeviceInfoDto> Subscribe(DeviceInfoDto input)
        {
            //add extra validatio logic here if required
            var saveModel = MapDtoToEntity(input);
            var result = await _repository.InsertAsync(saveModel);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapEntityToDto(result);
        }

        public async Task<DeviceInfoDto> Unsubscribe(DeviceInfoDto input)
        {
            var saveModel = MapEntityDtoToEntity(input); 
            await _repository.DeleteAsync(saveModel);
            await CurrentUnitOfWork.SaveChangesAsync();
            return input;
        }


        private DeviceInfo MapDtoToEntity(DeviceInfoDto input)
        {
            return new DeviceInfo
            {
                UserID = (long)AbpSession.UserId,
                HardwareModel = input.HardwareModel,
                PlatformType = input.PlatformType,
                PlatformVersion = input.PlatformVersion,
                RegID = input.RegID,
                UniqueDeviceId = input.UniqueDeviceId
            };
        }

        private DeviceInfoDto MapEntityToDto(DeviceInfo  input)
        {
            return new DeviceInfoDto
            { 
                Id = input.Id,
                HardwareModel = input.HardwareModel,
                PlatformType = input.PlatformType,
                PlatformVersion = input.PlatformVersion,
                RegID = input.RegID,
                UniqueDeviceId = input.UniqueDeviceId
            };
        }

        private DeviceInfo MapEntityDtoToEntity(DeviceInfoDto input)
        {
            return new DeviceInfo
            {
                Id = input.Id,
                UserID = (long)AbpSession.UserId,
                HardwareModel = input.HardwareModel,
                PlatformType = input.PlatformType,
                PlatformVersion = input.PlatformVersion,
                RegID = input.RegID,
                UniqueDeviceId = input.UniqueDeviceId
            };
        }
    }
}

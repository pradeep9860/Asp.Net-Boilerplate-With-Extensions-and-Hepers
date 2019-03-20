using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Helpers.PushNotification.Dto
{
    public class DeviceInfoDto  
    {
        public Guid Id { get; set; }
        [Required]
        public string RegID { get; set; } 
        public string HardwareModel { get; set; } 

        [Required]
        public string PlatformType { get; set; }
        public string PlatformVersion { get; set; }
        public string UniqueDeviceId { get; set; }
    }

}

using Abp.Domain.Entities.Auditing;
using Mongos.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Helpers.PushNotfication.Models
{
    public class DeviceInfo : FullAuditedEntity<Guid>
    {
        [Required]
        public string RegID { get; set; } 

        [ForeignKey("User")]
        public long UserID { get; set; }
        public virtual User User { get; set; }

        public string HardwareModel { get; set; }

        [Required]
        public string PlatformType { get; set; }
        public string PlatformVersion { get; set; }
        public string UniqueDeviceId { get; set; }
    } 

}
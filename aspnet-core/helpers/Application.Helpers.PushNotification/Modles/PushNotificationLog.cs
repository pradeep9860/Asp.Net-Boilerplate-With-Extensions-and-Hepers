using Abp.Domain.Entities.Auditing;
using Mongos.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Helpers.PushNotfication.Models
{
    public class PushNotificationLog : FullAuditedEntity<Guid>
    { 
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
         
        public DateTime NotificationSentOn { get; set; }
        public string NotificationContent { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadOn { get; set; } 
        public string ReferenceId { get; set; }
        public string NotificationBody { get; set; }
    }


}
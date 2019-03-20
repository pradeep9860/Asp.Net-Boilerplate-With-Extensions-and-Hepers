using System;

namespace Application.Helpers.PushNotification.Dto
{
    public class PushNotificationLogDto
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }  
        public DateTime NotificationSentOn { get; set; }
        public string NotificationContent { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadOn { get; set; }
        public string ReferenceId { get; set; }
        public NotificationJsonObj NotificationBody { get; set; }
    }

}

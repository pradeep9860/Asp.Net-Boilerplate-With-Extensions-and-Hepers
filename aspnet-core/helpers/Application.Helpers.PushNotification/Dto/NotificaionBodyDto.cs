using System;
using System.Collections.Generic;

namespace Application.Helpers.PushNotification.Dto
{
    public class NotificaionBodyDto  
    {  
        public string ReferenceId { get; set; }
        public Guid NotificationLogId { get; set; }
        public object custom_notification { get; set; }
        public string body { get; set; }
        public string title { get; set; }
        public string priority { get; set; }
        public bool content_available { get; set; }
        public string code { get; set; } 
        public List<long> UserIdList { get; set; }
    } 
   
    public class NotificationJsonObj 
    {
        public string to { get; set; }
        public string priority { get; set; } 
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificaionBodyDto notification { get; set; }
        public NotificaionBodyDto data { get; set; } 
    }
}

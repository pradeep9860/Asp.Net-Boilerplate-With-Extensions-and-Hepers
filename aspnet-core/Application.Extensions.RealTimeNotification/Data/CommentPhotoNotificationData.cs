using Abp.Notifications;
using System;

namespace Application.Extensions.RealTimeNotification.Data
{
    [Serializable]
    public class CommentPhotoNotificationData : NotificationData
    {
        private string commenterUserName;
        private string comment;
        private object p;
        private Guid photoId;

        public CommentPhotoNotificationData(string commenterUserName, string comment, object p, Guid photoId)
        {
            this.commenterUserName = commenterUserName;
            this.comment = comment;
            this.p = p;
            this.photoId = photoId;
        }
    }
}

using Abp;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.Notifications;
using Application.Extensions.RealTimeNotification.Data;
using System;
using System.Threading.Tasks;

namespace Application.Extensions.RealTimeNotification.Publisher
{
    public class PublisherService : ITransientDependency
    {
        private readonly INotificationPublisher _notiticationPublisher;

        public PublisherService(INotificationPublisher notiticationPublisher)
        {
            _notiticationPublisher = notiticationPublisher;
        }

        //Send a general notification to a specific user
        public async Task Publish_SentFrendshipRequest(string senderUserName, string friendshipMessage, UserIdentifier targetUserId)
        {
            await _notiticationPublisher.PublishAsync("SentFrendshipRequest", new SentFrendshipRequestNotificationData(senderUserName, friendshipMessage), userIds: new[] { targetUserId });
        }

        //Send an entity notification to a specific user
        public async Task Publish_CommentPhoto(string commenterUserName, string comment, Guid photoId, UserIdentifier photoOwnerUserId)
        {
            await _notiticationPublisher.PublishAsync("CommentPhoto", new CommentPhotoNotificationData(commenterUserName, comment, null,/*new EntityIdentifier(typeof(Photo),*/ photoId), userIds: new[] { photoOwnerUserId });
        }

        //Send a general notification to all subscribed users in current tenant (tenant in the session)
        public async Task Publish_LowDisk(int remainingDiskInMb)
        {
            //Example "LowDiskWarningMessage" content for English -> "Attention! Only {remainingDiskInMb} MBs left on the disk!"
            var data = new LocalizableMessageNotificationData(new LocalizableString("LowDiskWarningMessage", "MyLocalizationSourceName"));
            data["remainingDiskInMb"] = remainingDiskInMb;

            await _notiticationPublisher.PublishAsync("System.LowDisk", data, severity: NotificationSeverity.Warn);
        }
    }
}

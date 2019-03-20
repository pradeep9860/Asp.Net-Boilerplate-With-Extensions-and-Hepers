using Abp;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Notifications;
using System;
using System.Threading.Tasks;

namespace Application.Extensions.RealTimeNotification.Subscription
{
    public class SubscriberService : ITransientDependency
    {
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public SubscriberService(INotificationSubscriptionManager notificationSubscriptionManager)
        {
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }

        //Subscribe to a general notification
        public async Task Subscribe_SentFriendshipRequest(int? tenantId, long userId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId, userId), "SentFriendshipRequest");
        }

        //Subscribe to an entity notification
        public async Task Subscribe_CommentPhoto(int? tenantId, long userId, Guid photoId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId, userId), "CommentPhoto", null /*new EntityIdentifier(typeof(Photo), photoId)*/);
        }
    }
}

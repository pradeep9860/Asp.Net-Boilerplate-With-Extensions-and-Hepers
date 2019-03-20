using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Extensions.RealTimeNotification.NotificationProvider
{
    public class RealTimeAppNotificationProvider : Abp.Notifications.NotificationProvider
    {
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            context.Manager.Add(
                new NotificationDefinition(
                    "App.NewUserRegistered",
                    displayName: new LocalizableString("NewUserRegisteredNotificationDefinition", "MyLocalizationSourceName"),
                    permissionDependency: new SimplePermissionDependency("App.Pages.UserManagement")
                    )
                );
        }
    }
}
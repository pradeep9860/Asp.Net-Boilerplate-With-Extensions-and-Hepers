using Abp.Dependency;
using Application.Extensions.Hangfire.Jobs.Scheduler;
using Application.Helpers.MailHelper;
using Application.Helpers.PushNotfication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Extensions.Hangfire.Jobs
{
    public static class JobHelper
    { 
        public static void JobInitializer(IIocResolver iocResolver)
        {
            IMailHelper mailHelper = iocResolver.Resolve<IMailHelper>();
            IPushNotificationHelper pushNotificationHelper = iocResolver.Resolve<IPushNotificationHelper>();
            new MyTestScheduler(mailHelper, pushNotificationHelper).Run();
        } 
    }
}

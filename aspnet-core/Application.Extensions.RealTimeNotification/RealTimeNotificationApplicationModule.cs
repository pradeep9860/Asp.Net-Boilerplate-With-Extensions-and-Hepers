using Abp.Modules;
using Abp.Reflection.Extensions;
using Application.Extensions.RealTimeNotification.NotificationProvider;
using Mongos;

namespace Application.Extensions.RealTimeNotification
{
    [DependsOn(
       typeof(MongosCoreModule) 
       )] 
    public class RealTimeNotificationApplicationModule : AbpModule
    {  
        public override void PreInitialize()
        {
            Configuration.Notifications.Providers.Add<RealTimeAppNotificationProvider>();
        }

        public override void Initialize()
        { 
            IocManager.RegisterAssemblyByConvention(typeof(RealTimeNotificationApplicationModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            
        }
    }
}

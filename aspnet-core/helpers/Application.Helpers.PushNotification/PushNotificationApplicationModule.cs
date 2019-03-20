
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Application.Helpers.PushNotfication;
using Application.Helpers.PushNotfication.NotificationService;
using Application.Helpers.PushNotification.Dto;
using Mongos;

namespace PushNotfication
{
    [DependsOn(
    typeof(MongosCoreModule))]
    public class PushNotificationApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.Register<INotificationAppService, NotificationAppService>(DependencyLifeStyle.Singleton); 
            IocManager.Register<IPushNotificationHelper, PushNotificationHelper>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(typeof(PushNotificationApplicationModule).GetAssembly()); 
        }

        public override void PostInitialize()
        { 
        }
    }
}

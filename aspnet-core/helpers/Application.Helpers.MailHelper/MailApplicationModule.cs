
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Application.Helpers.MailHelper;
using Application.Helpers.MailHelper.EmailServices;
using Microsoft.Extensions.Configuration;
using Mongos;

namespace Emails
{
    [DependsOn(
    typeof(MongosCoreModule))]
    public class MailApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.Register<IEmailService, EmailService>(DependencyLifeStyle.Transient);
            IocManager.Register<IMailHelper, MailHelper>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(typeof(MailApplicationModule).GetAssembly()); 
        }

        public override void PostInitialize()
        { 
        }
    }
}

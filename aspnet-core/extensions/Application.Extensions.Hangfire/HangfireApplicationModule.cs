using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules; 
using Abp.Reflection.Extensions;
using Application.Extensions.Hangfire.Jobs;
using Emails;
using Hangfire.Storage;
using Microsoft.Extensions.Configuration;
using Mongos;
using Mongos.Configuration;
using Mongos.Web;
using PushNotfication;
using System;
using System.Transactions;

namespace Hangfire
{
    [DependsOn(
    typeof(MongosCoreModule),
    typeof(AbpHangfireAspNetCoreModule) ,
    typeof(MailApplicationModule),
    typeof(PushNotificationApplicationModule))]

    public class HangfireApplicationModule : AbpModule
    { 
        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.UseHangfire();

            //InvalidOperationException " JobStorage.Current property value has not been initialized"
            //var storage = JobStorage.Current;

            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            var connectionstring = configuration.GetConnectionString(MongosConsts.ConnectionStringName); 

            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionstring);
            //no exception
            var storage = JobStorage.Current;

            //for database connection remove the comment and test your application
            //try
            //{
            //    //for testing read all recurring jobs
            //    JobStorage.Current.GetConnection().GetRecurringJobs();
            //}
            //catch (System.Exception ex)
            //{

            //    throw ex;
            //}

            Configuration.UnitOfWork.IsolationLevel = IsolationLevel.ReadCommitted;
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HangfireApplicationModule).GetAssembly()); 
        }

        public override void PostInitialize()
        {
            JobHelper.JobInitializer(IocManager);
        }
    }
}

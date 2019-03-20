using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using Mongos.Authentication.JwtBearer;
using Mongos.Configuration;
using Mongos.EntityFrameworkCore;
using Application.Extensions.MongoDB;
using Abp.Configuration.Startup;
using Hangfire;
using Emails;
using PushNotfication;
using Application.Extensions.RealTimeNotification;

namespace Mongos
{
    [DependsOn(
         typeof(MongosApplicationModule),
          typeof(MongosEntityFrameworkModule),
          typeof(MongoDBApplicationModule),
         typeof(AbpAspNetCoreModule)
        ,typeof(AbpAspNetCoreSignalRModule)
        ,typeof(HangfireApplicationModule)
        ,typeof(MailApplicationModule)
        ,typeof(PushNotificationApplicationModule)
        ,typeof(RealTimeNotificationApplicationModule)
     )]
    public class MongosWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MongosWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                MongosConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(MongosApplicationModule).GetAssembly()
                 );

            Configuration.Modules.AbpAspNetCore()
               .CreateControllersForAppServices(
                   typeof(MailApplicationModule).GetAssembly()
               );

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(HangfireApplicationModule).GetAssembly()
                );


            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(PushNotificationApplicationModule).GetAssembly()
                );

            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MongosWebCoreModule).GetAssembly());
        }
    }
}

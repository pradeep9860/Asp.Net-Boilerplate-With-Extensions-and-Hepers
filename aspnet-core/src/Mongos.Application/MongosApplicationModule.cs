using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Mongos.Authorization; 

namespace Mongos
{
    [DependsOn(
        typeof(MongosCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MongosApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {

            Configuration.Authorization.Providers.Add<MongosAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.Register<Mongos.MongoAppService.IMongoAppService, Mongos.MongoAppService.MongoAppService>();
            var thisAssembly = typeof(MongosApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}

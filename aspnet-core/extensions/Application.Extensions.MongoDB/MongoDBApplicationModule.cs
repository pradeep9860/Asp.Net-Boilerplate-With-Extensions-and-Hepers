using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories; 
using Abp.Modules; 
using Abp.Reflection.Extensions;
using Application.Extensions.MongoDB.Model;
using Application.Extensions.MongoDB.Repositories;
using Application.Extensions.MongoDB.Seed;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using MongoDB.Bson;
using Mongos;
using Mongos.MongoAppService.Dto;
using System.Reflection;

namespace Application.Extensions.MongoDB
{
    [DependsOn(
        typeof(MongosCoreModule)
        , typeof(AbpAutoMapperModule)
        )]
     
    public class MongoDBApplicationModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            IocManager.Register<IMongoDbContext, MongoDbContext>();

            //if (!SkipDbContextRegistration)
            //{
            //    Configuration.Modules.AbpEfCore().AddDbContext<MongosDbContext>(options =>
            //    {
            //        if (options.ExistingConnection != null)
            //        {
            //            MongosDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
            //        }
            //        else
            //        {
            //            MongosDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            //        }
            //    });
            //}

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<MyTestModel, MyTestDto>() 
                      .ForMember(u => u.Id, options => options.MapFrom(input => input.Id));
            });
        }
         
        public override void Initialize()
        {
            //Transient
            IocManager.IocContainer.Register(
                Classes.FromAssembly(typeof(MongoDBApplicationModule).GetAssembly())
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );

            //Singleton
            IocManager.IocContainer.Register(
                Classes.FromAssembly(typeof(MongoDBApplicationModule).GetAssembly())
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
                );

            //Windsor Interceptors
            IocManager.IocContainer.Register(
                Classes.FromAssembly(typeof(MongoDBApplicationModule).GetAssembly())
                    .IncludeNonPublicTypes()
                    .BasedOn<IInterceptor>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .LifestyleTransient()
                );

            IocManager.IocContainer.Register(Component.For(typeof(IMongoRepository<,>)).ImplementedBy(typeof(MongoRepository<,>)));

            //IocManager.Register(typeof(IMongoRepository<IEntity<ObjectId> ,ObjectId>), typeof(MongoDbRepositoryBase<>) ,Abp.Dependency.DependencyLifeStyle.Singleton); 
            //IocManager.Register(typeof(IMongoRepository<MyTestModel, ObjectId>), typeof(MongoDbRepositoryBase<MyTestModel, ObjectId>), Abp.Dependency.DependencyLifeStyle.Transient);

            IocManager.RegisterAssemblyByConvention(typeof(MongoDBApplicationModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            SeedHelper.SeedHostDb(IocManager);
            //if (!SkipDbSeed)
            //{
                
            //}
        }
    }
}

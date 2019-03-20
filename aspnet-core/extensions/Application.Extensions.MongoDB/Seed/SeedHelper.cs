using Abp.Dependency;
using Abp.Domain.Uow;
using Application.Extensions.MongoDB.Seed.SeedBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Application.Extensions.MongoDB.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            var context = iocResolver.Resolve<IMongoDbContext>();
            new InitialHostMongoDbBuilder(context).Create();

            //// Default tenant seed (in host database).
            //new DefaultTenantBuilder(context).Create();
            //new TenantRoleAndUserBuilder(context, 1).Create();
        } 
    }
}

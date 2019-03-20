using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Mongos.Authorization.Roles;
using Mongos.Authorization.Users;
using Mongos.MultiTenancy;
using Application.Helpers.PushNotfication.Models;

namespace Mongos.EntityFrameworkCore
{
    public class MongosDbContext : AbpZeroDbContext<Tenant, Role, User, MongosDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MongosDbContext(DbContextOptions<MongosDbContext> options)
            : base(options)
        {
        }

         public DbSet<DeviceInfo> DeviceInfos { get; set; }
        public DbSet<PushNotificationLog> PushNotificationLogs { get; set; }
    }
}

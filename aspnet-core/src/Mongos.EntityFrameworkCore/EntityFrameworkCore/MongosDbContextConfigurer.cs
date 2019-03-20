using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Mongos.EntityFrameworkCore
{
    public static class MongosDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MongosDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MongosDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

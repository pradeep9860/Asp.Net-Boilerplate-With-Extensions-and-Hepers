using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Mongos.Configuration;
using Mongos.Web;

namespace Mongos.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MongosDbContextFactory : IDesignTimeDbContextFactory<MongosDbContext>
    {
        public MongosDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MongosDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MongosDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MongosConsts.ConnectionStringName));

            return new MongosDbContext(builder.Options);
        }
    }
}

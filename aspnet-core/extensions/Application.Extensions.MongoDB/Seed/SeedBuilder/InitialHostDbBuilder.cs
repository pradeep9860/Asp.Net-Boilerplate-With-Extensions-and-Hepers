namespace Application.Extensions.MongoDB.Seed.SeedBuilder
{
    public class InitialHostMongoDbBuilder
    {
        private readonly IMongoDbContext _context;

        public InitialHostMongoDbBuilder(IMongoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            //new DefaultLanguagesCreator(_context).Create();
            //new HostRoleAndUserCreator(_context).Create();
            //new DefaultSettingsCreator(_context).Create();

          
        }
    }
}

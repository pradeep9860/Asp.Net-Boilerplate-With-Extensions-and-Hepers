using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Application.Extensions.MongoDB.Model;
using Abp.Application.Editions;
using Abp.Application.Features;

namespace Application.Extensions.MongoDB
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Edition> Editions
        {
            get
            {
                return _database.GetCollection<Edition>("Editions");
            }
        }

        public IMongoCollection<EditionFeatureSetting> EditionFeatureSettings
        {
            get
            {
                return _database.GetCollection<EditionFeatureSetting>("EditionFeatureSettings");
            }
        }
    }

    public interface IMongoDbContext
    {
        IMongoCollection<Edition> Editions { get; }
        IMongoCollection<EditionFeatureSetting> EditionFeatureSettings { get; }
        
    }
}

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Editions;
using Abp.Application.Features;
using Mongos.Editions;
using MongoDB.Driver;

namespace Application.Extensions.MongoDB.Seed.SeedBuilder
{
    public class DefaultEditionCreator
    {
        private readonly IMongoDbContext _context;

        public DefaultEditionCreator(IMongoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private async void CreateEditions()
        {
            FilterDefinition<Edition> filter = Builders<Edition>.Filter.Eq(e => e.Name, EditionManager.DefaultEditionName); 
            var defaultEdition = await _context.Editions.FindSync(filter).FirstOrDefaultAsync();
            if (defaultEdition == null)
            {
                defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                await _context.Editions.InsertOneAsync(defaultEdition);
               
                /* Add desired features to the standard edition, if wanted... */
            }
        }

        private async void CreateFeatureIfNotExists(int editionId, string featureName, bool isEnabled)
        {
            FilterDefinition<EditionFeatureSetting> filter = Builders<EditionFeatureSetting>.Filter.Eq(e => e.EditionId, editionId);
            if (await _context.EditionFeatureSettings.FindSync(filter).AnyAsync())
            {
                return;
            }

            await _context.EditionFeatureSettings.InsertOneAsync(new EditionFeatureSetting
            {
                Name = featureName,
                Value = isEnabled.ToString(),
                EditionId = editionId
            }); 
        }
    }
}
